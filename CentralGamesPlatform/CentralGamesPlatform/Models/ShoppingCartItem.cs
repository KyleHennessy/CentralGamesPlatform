using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class ShoppingCartItem
	{
		public int ShoppingCartItemId { get; set; }
		public string ShoppingCartId { get; set; }
		public Game Game { set; get; }
		public int Amount { get; set; }
	}
}
