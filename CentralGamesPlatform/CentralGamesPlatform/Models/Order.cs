using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class Order
	{
		[BindNever]
		public int OrderId { get; set; }

		[Required(ErrorMessage = "Required field")]
		[Display(Name = "First Name")]
		[StringLength(25)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Required field")]
		[Display(Name = "Last Name")]
		[StringLength(50)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Required field")]
		[Display(Name = "Street Address")]
		[StringLength(100)]
		public string Address { get; set; }

		[Required(ErrorMessage = "Required field")]
		[StringLength(50)]
		public string Town { get; set; }

		[Required(ErrorMessage = "Required field")]
		[StringLength(50)]
		public string County { get; set; }

		[Required(ErrorMessage = "Required field")]
		[StringLength(10)]
		public string EirCode { get; set; }

		[Required(ErrorMessage = "Required field")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		public List<OrderDetail> OrderDetails { get; set; }

		[BindNever]
		public decimal OrderTotal { get; set; }
		
		[BindNever]
		public DateTime OrderPlaced { get; set; }

		public bool IsSuccessful { get; set; }
	}
}
