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
		private readonly MyDatabaseContext _myDatabaseContext;
		//private readonly string WebHookSecret = "whsec_s5NmxKzkSFLlmSiqnkJiMxLqnwM9QeOA";
		public PaymentController(IOrderRepository orderRepository, ShoppingCart shoppingCart, IPaymentRepository paymentRepository, 
								 IOrderDetailRepository orderDetailRepository, ILicenceRepository licenseRepository, MyDatabaseContext myDatabaseContext)
		{
			_orderRepository = orderRepository;
			_shoppingCart = shoppingCart;
			_paymentRepository = paymentRepository;
			_orderDetailRepository = orderDetailRepository;
			_licenseRepository = licenseRepository;
			_myDatabaseContext = myDatabaseContext;
			
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
				SuccessUrl = "https://localhost:44394/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
				CancelUrl = "https://localhost:44394/Payment/Failed",
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
				Payment payment = new Payment();
				Models.Order order = _orderRepository.GetOrder(orderId);
				decimal total = order.OrderTotal;
				_paymentRepository.CreatePayment(sessionId, payment, orderId, total);
				_orderRepository.SuccessfulOrder(orderId);
				var orderDetails = _orderDetailRepository.GetAllOrderDetails(orderId);
				_licenseRepository.CreateLicense(orderDetails, userId);
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
