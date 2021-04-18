using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.IRepositories
{
    public interface IPayoutRepository
    {
        void CreatePayout(Payout payout);
    }
}
