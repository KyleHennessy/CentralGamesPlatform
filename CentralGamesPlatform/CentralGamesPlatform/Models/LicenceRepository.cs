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
		public void CreateLicense(List<OrderDetail> orderDetails, string userId)
		{
			foreach (var orderDetail in orderDetails)
			{
				
				int orderDetailId = orderDetail.OrderDetailId;
				Guid licenseKey = Guid.NewGuid();

				var license = new Licence
				{
					LicenseKey = licenseKey,
					OrderDetailId = orderDetailId,
					UserId = userId
				};
				_myDatabaseContext.Licences.Add(license);
				_myDatabaseContext.SaveChanges();
			}
		}

        public IEnumerable<Licence> GetLicences(string userId)
        {
			var licences = (from l in _myDatabaseContext.Licences
							where l.UserId == userId
							select l).ToList();
			return licences;
        }
    }
}
