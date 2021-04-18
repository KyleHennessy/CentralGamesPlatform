using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.IRepositories
{
	public interface ILicenceRepository
	{
		void CreateLicense(List<OrderDetail> orderDetails, string userId);
		public IEnumerable<Licence> GetLicences(string userId);
	}
}
