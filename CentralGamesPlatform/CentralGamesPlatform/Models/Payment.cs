using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class Payment
	{
		[BindNever]
		public int PaymentId { get; set; }
		public int OrderId { get; set; }
		public string StripeSession { get; set; }
		public decimal Total { get; set; }
		public DateTime PaymentDateTime { get; set; }
	}
}
