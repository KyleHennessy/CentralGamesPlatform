using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class Licence
	{
		[BindNever]
		public int LicenceId { get; set; }
		public Guid LicenseKey { get; set; }
		public int OrderDetailId { get; set; }
	}
}
