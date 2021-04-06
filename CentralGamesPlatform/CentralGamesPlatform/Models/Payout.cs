using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class Payout
	{
        [BindNever]
        public int PayoutId { get; set; }
        public int WalletId { get; set; }

        [Required(ErrorMessage = "Required field, must be greater be between 5 and 100")]
        [Display(Name = "Transfer Amount")]
        [DataType(DataType.Currency)]
        [Range(5,100)]
        public decimal AmountTransfered { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "PayPal Email")]
        [EmailAddress]
        public string PayPalEmail { get; set; }
        public string PayPalBatchId { get; set; }
        public DateTime PayoutDate { get; set; }
        
    }
}
