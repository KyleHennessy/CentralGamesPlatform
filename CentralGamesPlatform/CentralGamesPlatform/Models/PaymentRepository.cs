using Newtonsoft.Json.Linq;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class PaymentRepository : IPaymentRepository
	{
		private readonly MyDatabaseContext _myDatabaseContext;

		public PaymentRepository(MyDatabaseContext myDatabaseContext)
		{
			_myDatabaseContext = myDatabaseContext;
		}

		public void CreatePayment(string stripeSession, Payment payment, int orderid, decimal total)
		{
			payment.OrderId = orderid;
			payment.StripeSession = stripeSession;
			payment.Total = total;
			payment.PaymentDateTime = DateTime.Now;

			_myDatabaseContext.Payments.Add(payment);
			_myDatabaseContext.SaveChanges();
		}
	}
}
