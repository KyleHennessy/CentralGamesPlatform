using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class OrderRepository : IOrderRepository
	{
		private readonly MyDatabaseContext _myDatabaseContext;
		private readonly ShoppingCart _shoppingCart;

		public OrderRepository(MyDatabaseContext myDatabaseContext, ShoppingCart shoppingCart)
		{
			_myDatabaseContext = myDatabaseContext;
			_shoppingCart = shoppingCart;
		}
		public void CreateOrder(Order order)
		{
			order.OrderPlaced = DateTime.Now;
			order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
			_myDatabaseContext.Orders.Add(order);
			_myDatabaseContext.SaveChanges();

			var shoppingCartItems = _shoppingCart.GetShoppingCartItems();

			foreach(var cartItem in shoppingCartItems)
			{
				if (cartItem.Game.GameId == -1)
				{
					var orderDetail = new OrderDetail
					{
						Amount = cartItem.Amount,
						Price = cartItem.Game.Price,
						GameId = cartItem.Game.GameId,
						OrderId = order.OrderId,
						CasinoPass = true
					};
					_myDatabaseContext.OrderDetails.Add(orderDetail);
				}
				else
				{
					var orderDetail = new OrderDetail
					{
						Amount = cartItem.Amount,
						Price = cartItem.Game.Price,
						GameId = cartItem.Game.GameId,
						OrderId = order.OrderId
					};
					_myDatabaseContext.OrderDetails.Add(orderDetail);
				}

				
			}

			_myDatabaseContext.SaveChanges();
		}
		public void SuccessfulOrder(int orderId)
		{
			Order result = (from o in _myDatabaseContext.Orders 
							where o.OrderId == orderId select o).SingleOrDefault();

			result.IsSuccessful = true;

			_myDatabaseContext.SaveChanges();
		}
		public Order GetOrder(int orderId)
		{
			Order result = (from o in _myDatabaseContext.Orders
							where o.OrderId == orderId
							select o).SingleOrDefault();
			return result;
		}

        public List<Order> GetUsersOrders(string userid)
        {
			var orders = (from o in _myDatabaseContext.Orders
										 where o.UserId == userid
										 select o).ToList();
			return orders;
        }
    }
}
