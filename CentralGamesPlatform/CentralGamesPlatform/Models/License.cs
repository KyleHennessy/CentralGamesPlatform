using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class License
	{
		[BindNever]
		public Guid LicenseId { get; set; }
		public int OrderDetailId { get; set; }
	}
}
