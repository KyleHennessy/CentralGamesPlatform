using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    [Authorize]
    public class StartController : Controller
    {
        private readonly ICasinoPassRepository _casinoPassRepository;

        public StartController(ICasinoPassRepository casinoPassRepository)
        {
            _casinoPassRepository = casinoPassRepository;
        }
        public RedirectToActionResult GiveAccessToCasinoGame(int gameId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ownedPasses = _casinoPassRepository.GetCasinoPassesByUserId(userId);

            //CasinoPass activePass = ownedPasses.FirstOrDefault(p => (p.Active == true) && (p.Expired == false) && (p.GameId == 1));
            CasinoPass activePass = ownedPasses.FirstOrDefault(p => (p.GameId == gameId) && (p.Active == true) && (p.Expired == false)) ;
            //if its active already
            if (activePass != null)
            {
                return RedirectToAction("Index", "Play", new { area = "CoinFlip", casinoPassId = activePass.CasinoPassId });
            }
            else
            {
                CasinoPass firstValidPass = ownedPasses.Where(p => p.Active == false)
                                            .Where(p => p.Expired == false)
                                            .Where(p => p.GameId == 0)
                                            .FirstOrDefault();
                //if no valid passes owned redirect to game pass page
                if (firstValidPass == null)
                {
                    return RedirectToAction("Details", "Game", new { id = -1 });
                }
                //activate pass and assign game to pass, redirect to game library
                else
                {
                    Guid casinoPassId = firstValidPass.CasinoPassId;
                    _casinoPassRepository.ActivateCasinoPass(casinoPassId, gameId);
                    //return RedirectToAction("Index", "Play", new { area = "CoinFlip", passId = casinoPassId });
                    return RedirectToAction("Index", "Library");
                }
            }
        }
    }
}
