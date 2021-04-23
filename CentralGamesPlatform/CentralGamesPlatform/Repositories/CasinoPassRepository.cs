using CentralGamesPlatform.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class CasinoPassRepository : ICasinoPassRepository
    {
        private readonly MyDatabaseContext _myDatabaseContext;

        public CasinoPassRepository(MyDatabaseContext myDatabaseContext)
        {
            _myDatabaseContext = myDatabaseContext;
        }
        public void CreateCasinoPass(CasinoPass casinoPass)
        {
            casinoPass.PassPlaced = DateTime.Now;
            _myDatabaseContext.CasinoPasses.Add(casinoPass);
        }

        public void ActivateCasinoPass(Guid casinoPassId, int gameId)
        {
            CasinoPass pass = (from p in _myDatabaseContext.CasinoPasses
                               where p.CasinoPassId == casinoPassId select p).Single();
            pass.Active = true;
            pass.GameId = gameId;
            _myDatabaseContext.SaveChanges();
        }

        public void ExpireCasinoPass(Guid casinoPassId)
        {
            CasinoPass pass = (from p in _myDatabaseContext.CasinoPasses
                               where p.CasinoPassId == casinoPassId
                               select p).Single();
            pass.Active = false;
            pass.Expired = true;
            _myDatabaseContext.SaveChanges();
        }

        public CasinoPass GetCasinoPass(Guid casinoPassId)
        {
            CasinoPass pass = (from p in _myDatabaseContext.CasinoPasses
                               where p.CasinoPassId == casinoPassId
                               select p).SingleOrDefault();
            return pass;
        }

        public IEnumerable<CasinoPass> GetCasinoPassesByUserId(string userId)
        {
            var passes = (from p in _myDatabaseContext.CasinoPasses
                          where p.UserId == userId
                          select p).ToList();
            return passes;
        }

        public int AmountPurchasedToday(string userId)
        {
            DateTime todaysDateTime = DateTime.Today; //Todays date and time at 00:00:00
            DateTime yesterdaysDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Todays date and time at 23:59:59
            var count = (from p in _myDatabaseContext.CasinoPasses.OrderByDescending(p => p.PassPlaced)
                         where (p.PassPlaced > todaysDateTime && p.PassPlaced <= yesterdaysDateTime)
                         select p).Count();
            return count;
        }
    }
}
