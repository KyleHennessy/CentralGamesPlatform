using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public interface IPaymentRepository
	{
		void CreatePayment(string stripeSession, Payment payment, int orderid, decimal total);
	}
}
