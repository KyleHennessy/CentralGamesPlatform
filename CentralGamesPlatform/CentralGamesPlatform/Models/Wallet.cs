using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
