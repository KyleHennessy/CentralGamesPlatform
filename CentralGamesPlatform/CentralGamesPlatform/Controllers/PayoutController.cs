using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayoutsSdk.Payouts;
using PayoutsSdk.Core;
using PayPalHttp;

using System.IO;
using System.Text;
using CentralGamesPlatform.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CentralGamesPlatform.Controllers
{
	[Authorize]
	public class PayoutController : Controller
	{
        private readonly IPayoutRepository _payoutRepository;
		private readonly IWalletRepository _walletRepository;
        public PayoutController(IPayoutRepository payoutRepository, IWalletRepository walletRepository)
		{
            _payoutRepository = payoutRepository;
			_walletRepository = walletRepository;
            //StripeConfiguration.ApiKey = "sk_test_51IB0Z4BTwx1LYfRRop1pYWwRVKBAs0K7KZBRbKTubudFUXJPN5BlooRahipg8qIkpIQ49d6c4YZE9ErcziO23QtR00rzwq6cbk";
        }
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(Payout submittedPayout)
        {
			string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			decimal walletBalance = _walletRepository.RetrieveBalance(userId);
			int walletId = _walletRepository.RetrieveWalletId(userId);
			if (walletId == 0)
            {
				ViewBag.ErrorMessage = "You have never won any money in a casino game before. Come back here when you have";
				return View();
            }
			submittedPayout.WalletId = walletId;
			if(submittedPayout.AmountTransfered < walletBalance && submittedPayout.AmountTransfered >= 5.00M)
            {
				string amountTransfered = submittedPayout.AmountTransfered.ToString();
				string email = submittedPayout.PayPalEmail;
				var response = CreatePayout(amountTransfered, email);
				HttpResponse createPayoutResponse = response.Result;
				var payout = createPayoutResponse.Result<CreatePayoutResponse>();

				submittedPayout.PayPalBatchId = payout.BatchHeader.PayoutBatchId;
				//_walletRepository.SubtractFromWallet(userId, submittedPayout.AmountTransfered);
				_payoutRepository.CreatePayout(submittedPayout);
				ViewBag.SuccessMessage = "You have successfully transfered €" + amountTransfered + " to your PayPal account";
			}
            else
            {
				ViewBag.ErrorMessage = "We were unable to transfer funds into the PayPal account with the email provided. Do you have enough in your wallet?";
            }
			return View();
        }

		private static CreatePayoutRequest buildRequestBody(string amountTransfered, string email)
		{
			var request = new CreatePayoutRequest()
			{

				SenderBatchHeader = new SenderBatchHeader()
				{
					EmailMessage = "You have received the following amount from Central Games Platform: €" + amountTransfered,
					EmailSubject = "Central Games Platform transfer"
				},
				Items = new List<PayoutItem>(){
					new PayoutItem(){
						RecipientType="EMAIL",

						Amount=new Currency(){
							CurrencyCode="EUR",
							Value=amountTransfered,
						 },
						Receiver=email,

					}	
				}
			};
			return request;

		}
		public async static Task<HttpResponse> CreatePayout(string amountTransfered, string email)
		{
			try
			{
				PayoutsPostRequest request = new PayoutsPostRequest();
				request.RequestBody(buildRequestBody(amountTransfered, email));
				var response = await PayPalClient.client().Execute(request);
				var result = response.Result<CreatePayoutResponse>();
				return response;
			}
			catch (HttpException ex)
			{
				return null;
			}
		}

		public async static Task<HttpResponse> GetPayout(string batchId,bool debug = false)
        {
            //try
            //{
				PayoutsGetRequest request = new PayoutsGetRequest(batchId);
				var getResponse = await PayPalClient.client().Execute(request);
				var result = getResponse.Result<PayoutBatch>();
                if (debug)
                {
					//do something
                }
				return getResponse;

            //}
			//catch (HttpException ex)
            //{
			//	return null;
            //}
        }

		public async static Task<HttpResponse> GetPayoutItem(string itemId, bool debug = false)
        {
			try
			{
				PayoutsItemGetRequest request = new PayoutsItemGetRequest(itemId);
				var getResponse = await PayPalClient.client().Execute(request);
				var result = getResponse.Result<PayoutItemResponse>();
				if (debug)
				{
					//do something

				}
				return getResponse;
			}
			catch (HttpException ex)
			{
				return null;
			}
		}






		//public IActionResult CreateAccount()
		//{
		//	return View();
		//}
		//[HttpPost]
		//public IActionResult CreateAccount(string email)
		//{
		//	//if (!_payoutRepository.ConnectAccountExists)
		//	//{
		//		var options = new AccountCreateOptions
		//		{
		//			Type = "express",
		//			Email = email,
		//		};
		//		var service = new AccountService();
		//		var accountService = service.Create(options);
		//		string accountId = accountService.Id;
		//		//_userRepository.AddNewUser();

		//		var linkoptions = new AccountLinkCreateOptions
		//		{
		//			Account = accountId,
		//			RefreshUrl = "https://localhost:44394/Payout/CreateAccount",
		//			ReturnUrl = "https://localhost:44394/Payout/Index",
		//			Type = "account_onboarding"
		//		};
		//		var linkservice = new AccountLinkService();
		//		var accountLink = linkservice.Create(linkoptions);
		//		return Redirect(accountLink.Url);
		//	//}

		//	//return View();
		//}
	}
}
