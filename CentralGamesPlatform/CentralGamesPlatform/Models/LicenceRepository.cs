using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class LicenceRepository : ILicenceRepository
	{
		private readonly MyDatabaseContext _myDatabaseContext;
		public LicenceRepository(MyDatabaseContext myDatabaseContext)
		{
			_myDatabaseContext = myDatabaseContext;
		}
		public void CreateLicense(List<OrderDetail> orderDetails)
		{
			foreach (var orderDetail in orderDetails)
			{
				
				int orderDetailId = orderDetail.OrderDetailId;
				Guid licenseKey = Guid.NewGuid();

				var license = new Licence
				{
					LicenseKey = licenseKey,
					OrderDetailId = orderDetailId
				};
				_myDatabaseContext.Licences.Add(license);
			}
		}
	}
}
