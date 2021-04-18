using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.IRepositories
{
	public interface IOrderRepository
	{
		void CreateOrder(Order order);
		void SuccessfulOrder(int orderId);
		Order GetOrder(int orderId);
		List<Order> GetUsersOrders(string userid);

	}
}
