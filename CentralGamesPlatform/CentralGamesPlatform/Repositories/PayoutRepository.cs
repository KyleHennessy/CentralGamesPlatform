using CentralGamesPlatform.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class PayoutRepository : IPayoutRepository
    {
        private readonly MyDatabaseContext _myDatabaseContext;
        public PayoutRepository(MyDatabaseContext myDatabaseContext)
        {
            _myDatabaseContext = myDatabaseContext;
        }
        public void CreatePayout(Payout payout)
        {
            payout.PayoutDate = DateTime.Now;

            _myDatabaseContext.Payouts.Add(payout);
            _myDatabaseContext.SaveChanges();
        }
    }
}
