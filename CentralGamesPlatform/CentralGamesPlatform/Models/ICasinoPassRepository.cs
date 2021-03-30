using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public interface ICasinoPassRepository
    {
        void CreateCasinoPass(CasinoPass casinoPass);
        IEnumerable<CasinoPass> GetCasinoPassesByUserId(string userId);
        CasinoPass GetCasinoPass(Guid casinoPassId);
        void ActivateCasinoPass(Guid casinoPassId, int gameId);
        void ExpireCasinoPass(Guid casinoPassId);
    }
}
