using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using System.IO;
using Microsoft.Extensions.Logging;
using Elmah.Io.AspNetCore;
using Stripe.Checkout;

namespace CentralGamesPlatform.Controllers
{
	public class PaymentController : Controller
	{
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrderRepository _orderRepository;
		private readonly string WebHookSecret = "whsec_s5NmxKzkSFLlmSiqnkJiMxLqnwM9QeOA";
		public PaymentController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
		{
			_orderRepository = orderRepository;
			_shoppingCart = shoppingCart;
			StripeConfiguration.ApiKey = "sk_test_51IB0Z4BTwx1LYfRRop1pYWwRVKBAs0K7KZBRbKTubudFUXJPN5BlooRahipg8qIkpIQ49d6c4YZE9ErcziO23QtR00rzwq6cbk";
		}
		public IActionResult Index( Models.Order order, string total)
		{
			return View(order);
		}

		[HttpPost("create-checkout-session")]
		public IActionResult CreateCheckoutSession(/*Models.Order order*/)
		{
			//Models.Order order = (Models.Order)TempData["order"];
			decimal orderTotal;
			_shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
			orderTotal = _shoppingCart.GetShoppingCartTotal() * 100;
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
				SuccessUrl = "https://localhost:44394/Payment/Success",
				CancelUrl = "https://localhost:44394/Payment/Failed",
			};
			var service = new SessionService();
			Session session = service.Create(options);
			//_orderRepository.CreateOrder(order);
			return Json(new { id = session.Id });
			
		}

		public IActionResult Success(/*Models.Order order*/)
		{
			//_orderRepository.CreateOrder(order);
			_shoppingCart.ClearCart();
			return View();
		}

		public IActionResult Failed()
		{
			return View();
		}

	}
}
