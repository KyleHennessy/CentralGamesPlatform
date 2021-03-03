using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class OrderDetailRepository : IOrderDetailRepository
	{
		private readonly MyDatabaseContext _myDatabaseContext;
		public OrderDetailRepository(MyDatabaseContext myDatabaseContext)
		{
			_myDatabaseContext = myDatabaseContext;
		}
		public List<OrderDetail> GetAllOrderDetails(int orderId)
		{
			var orderDetails = (from od in _myDatabaseContext.OrderDetails
								where od.OrderId == orderId
								select od).ToList();
			return orderDetails;
		}
	}
}
