using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class ShoppingCart
	{
		private readonly MyDatabaseContext _myDatabaseContext;
		public string ShoppingCartId { get; set; }
		public List<ShoppingCartItem> ShoppingCartItems { get; set; }

		public ShoppingCart(MyDatabaseContext myDatabaseContext)
		{
			_myDatabaseContext = myDatabaseContext;
		}

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>
				()?.HttpContext.Session;

			var context = services.GetService<MyDatabaseContext>();
			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
			session.SetString("CartId", cartId);
			return new ShoppingCart(context) { ShoppingCartId = cartId };
		}

		public void AddToCart(Game game, int amount)
		{
			var shoppingCartItem = _myDatabaseContext.ShoppingCartItems.SingleOrDefault(
				s => s.Game.GameId == game.GameId && s.ShoppingCartId == ShoppingCartId);

			if(shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem
				{
					ShoppingCartId = ShoppingCartId,
					Game = game,
					Amount = amount
				};

				_myDatabaseContext.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				shoppingCartItem.Amount++;
			}

			_myDatabaseContext.SaveChanges();
		}

		public int RemoveFromCart(Game game)
		{
			var shoppingCartItem = _myDatabaseContext.ShoppingCartItems.SingleOrDefault(
				s => s.Game.GameId == game.GameId && s.ShoppingCartId == ShoppingCartId);

			var localAmount = 0;
			if(shoppingCartItem != null)
			{
				if(shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}
				else
				{
					_myDatabaseContext.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_myDatabaseContext.SaveChanges();
			return localAmount;
		}

		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ?? (ShoppingCartItems = _myDatabaseContext.ShoppingCartItems.Where(c
				=> c.ShoppingCartId == ShoppingCartId)
				.Include(g => g.Game)
				.ToList());
		}

		public void ClearCart()
		{
			var cartItems = _myDatabaseContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId);

			_myDatabaseContext.ShoppingCartItems.RemoveRange(cartItems);
			_myDatabaseContext.SaveChanges();
		}

		public decimal GetShoppingCartTotal()
		{
			var total = _myDatabaseContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
				.Select(g => g.Game.Price * g.Amount).Sum();

			return total;
		}
	}


}
