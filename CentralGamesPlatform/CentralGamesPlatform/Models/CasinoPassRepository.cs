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
            throw new NotImplementedException();
        }

        public void ExpireCasinoPass(Guid casinoPassId)
        {
            throw new NotImplementedException();
        }

        public CasinoPass GetCasinoPass(Guid casinoPassId)
        {
            throw new NotImplementedException();
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
