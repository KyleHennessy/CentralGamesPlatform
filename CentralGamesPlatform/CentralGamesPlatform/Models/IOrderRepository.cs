using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public interface IOrderRepository
	{
		void CreateOrder(Order order);
		void SuccessfulOrder(int orderId);

		Order GetOrder(int orderId);

	}
}
