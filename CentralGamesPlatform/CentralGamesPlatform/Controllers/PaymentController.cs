using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using System.IO;
using Microsoft.Extensions.Logging;
using Stripe.Checkout;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CentralGamesPlatform.ViewModels;
using CentralGamesPlatform.IRepositories;

namespace CentralGamesPlatform.Controllers
{
	[Authorize]
	public class PaymentController : Controller
	{
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrderRepository _orderRepository;
		private readonly IPaymentRepository _paymentRepository;
		private readonly IOrderDetailRepository _orderDetailRepository;
		private readonly ILicenceRepository _licenseRepository;
		private readonly ICasinoPassRepository _casinoPassRepository;
		public PaymentController(IOrderRepository orderRepository, ShoppingCart shoppingCart, IPaymentRepository paymentRepository, 
								 IOrderDetailRepository orderDetailRepository, ILicenceRepository licenseRepository, 
								 ICasinoPassRepository casinoPassRepository)
		{
			_orderRepository = orderRepository;
			_shoppingCart = shoppingCart;
			_paymentRepository = paymentRepository;
			_orderDetailRepository = orderDetailRepository;
			_licenseRepository = licenseRepository;
			_casinoPassRepository = casinoPassRepository;
			
			
			StripeConfiguration.ApiKey = "sk_test_51IB0Z4BTwx1LYfRRop1pYWwRVKBAs0K7KZBRbKTubudFUXJPN5BlooRahipg8qIkpIQ49d6c4YZE9ErcziO23QtR00rzwq6cbk";
		}
		public IActionResult Index(int id)
		{
	
			_shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
			var paymentSummaryViewModel = new PaymentSummaryViewModel
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
				Order = _orderRepository.GetOrder(id)
			};
			return View(paymentSummaryViewModel);
		}

		[HttpPost("create-checkout-session")]
		public IActionResult CreateCheckoutSession()
		{
			decimal orderTotal;
			_shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
			orderTotal = _shoppingCart.GetShoppingCartTotal() * 100;
			if(orderTotal == 0.00M)
            {
				return RedirectToAction("Success");
            }
			var options = new SessionCreateOptions
			{
				PaymentMethodTypes = new List<string>
				{
					"card"
				},
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long?)orderTotal,
							Currency = "eur",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = "Order Total"
							},

						},
						Quantity = 1
					},
				},
				Mode = "payment",
                //SuccessUrl = "https://localhost:44394/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
                //CancelUrl = "https://localhost:44394/Payment/Failed"
                SuccessUrl = "https://centralgamesplatform.azurewebsites.net/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://centralgamesplatform.azurewebsites.net/Payment/Failed",
            };
			var service = new SessionService();
			Session session = service.Create(options);
			return Json(new { id = session.Id });	
		}

		
		public IActionResult Success()
		{
			if(TempData["orderId"] == null)
			{
				return RedirectToAction("Failed");
			}
			try
			{
				int orderId = (int)TempData["orderId"];
				string sessionId = HttpContext.Request.Query["session_id"];
				TempData.Clear();
				string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				//Create payment object in database
				Payment payment = new Payment();
				Models.Order order = _orderRepository.GetOrder(orderId);
				decimal total = order.OrderTotal;
				_paymentRepository.CreatePayment(sessionId, payment, orderId, total);
				//Update order to be sucessful
				_orderRepository.SuccessfulOrder(orderId);
				//Generate License keys for each game/pass purchased
				var orderDetails = _orderDetailRepository.GetAllOrderDetails(orderId);
				_licenseRepository.CreateLicense(orderDetails, userId);
				//If pass was purchased then create pass in database
				foreach (var orderDetail in orderDetails)
				{
					if (orderDetail.GameId == -1)
                    {
						CasinoPass casinoPass = new CasinoPass
						{
							UserId = userId,
							Active = false,
							Expired = false
						};
						_casinoPassRepository.CreateCasinoPass(casinoPass);
					}
				}
				_shoppingCart.ClearCart();
				return View();
			}

			catch(Exception ex)
			{
				return View(ex.Message);
			}
		}

		public IActionResult Failed()
		{
			return View();
		}

	}
}
