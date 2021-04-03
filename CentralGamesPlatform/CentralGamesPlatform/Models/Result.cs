using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class Result
    {
        [BindNever]
        public int ResultId { get; set; }
        public Guid CasinoPassId { get; set; }
        public bool Win  { get; set; }
        public decimal AmountWon { get; set; }
        public DateTime DateResultPlaced { get; set; }
    }
}
