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

		public IActionResult CreatePayout()
		{
			var options = new PayoutCreateOptions
			{
				Amount = 1100,
				Currency = "eur",
				Description = "Stripe Payout"
			};
			return View();
		}
	}
}
