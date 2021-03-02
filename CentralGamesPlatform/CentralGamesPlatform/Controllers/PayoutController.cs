using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
	public class PayoutController : Controller
	{
		//private readonly IPayoutRepository _payoutRepository;
		public PayoutController(/*IPayoutRepository payoutRepository*/)
		{
			//_payoutRepository = payoutRepository;
			StripeConfiguration.ApiKey = "sk_test_51IB0Z4BTwx1LYfRRop1pYWwRVKBAs0K7KZBRbKTubudFUXJPN5BlooRahipg8qIkpIQ49d6c4YZE9ErcziO23QtR00rzwq6cbk";
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CreateAccount()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CreateAccount(string email)
		{
			//if (!_payoutRepository.ConnectAccountExists)
			//{
				var options = new AccountCreateOptions
				{
					Type = "express",
					Email = email,
				};
				var service = new AccountService();
				var accountService = service.Create(options);
				string accountId = accountService.Id;
				//_userRepository.AddNewUser();

				var linkoptions = new AccountLinkCreateOptions
				{
					Account = accountId,
					RefreshUrl = "https://localhost:44394/Payout/CreateAccount",
					ReturnUrl = "https://localhost:44394/Payout/Index",
					Type = "account_onboarding"
				};
				var linkservice = new AccountLinkService();
				var accountLink = linkservice.Create(linkoptions);
				return Redirect(accountLink.Url);
			//}
			
			//return View();
		}
	}
}
