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
    }
}
