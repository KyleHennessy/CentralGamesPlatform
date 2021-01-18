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

namespace CentralGamesPlatform.Controllers
{
	public class PaymentController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly string WebHookSecret = "whsec_s5NmxKzkSFLlmSiqnkJiMxLqnwM9QeOA";
		public PaymentController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}
		private decimal amount = 100.00M;
		public IActionResult Index(decimal total, Models.Order order)
		{
			amount = total;
			return View(order);
		}

		[HttpPost]
		public IActionResult Processing(string stripeToken, string stripeEmail)
		{
			Dictionary<string, string> Metadata = new Dictionary<string, string>();
			Metadata.Add("Product", "Game");
			Metadata.Add("Quantity", "1");
			var options = new ChargeCreateOptions
			{
				Amount = (long?)amount,
				Currency = "EUR",
				Description = "Buying 1 game",
				Source = stripeToken,
				ReceiptEmail = stripeEmail,
				Metadata = Metadata
			};
			var service = new ChargeService();
			Charge charge = service.Create(options);
			return View();
		}
		[HttpPost]
		public IActionResult ChargeChange()
		{
			var json = new StreamReader(HttpContext.Request.Body).ReadToEnd();

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WebHookSecret, throwOnApiVersionMismatch: true);
				Charge charge = (Charge)stripeEvent.Data.Object;
				switch (charge.Status)
				{
					case "succeeded":
						charge.Metadata.TryGetValue("Product", out string Product);
						charge.Metadata.TryGetValue("Quantity", out string Quantity);
						break;
					case "failed":
						//TODO failed code
						break;
				}
			}
			catch(Exception e)
			{
				e.Ship(HttpContext);
				return BadRequest();
			}
			return Ok();
		}
	}
}
