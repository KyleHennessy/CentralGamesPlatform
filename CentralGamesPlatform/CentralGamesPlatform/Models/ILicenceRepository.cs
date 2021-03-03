using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public interface ILicenceRepository
	{
		void CreateLicense(List<OrderDetail> orderDetails);
	}
}
