using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    [Authorize]
    public class StartController : Controller
    {
        private readonly ICasinoPassRepository _casinoPassRepository;
        private readonly IVerificationRepository _verificationRepository;
        private readonly IGameRepository _gameRepository;

        public StartController(ICasinoPassRepository casinoPassRepository, IVerificationRepository verificationRepository,
                               IGameRepository gameRepository)
        {
            _casinoPassRepository = casinoPassRepository;
            _verificationRepository = verificationRepository;
            _gameRepository = gameRepository;
        }
        public RedirectToActionResult GiveAccessToCasinoGame(int gameId)
        {
            //Due to coinflip being the only playable casino game, I've hard coded it in so that
            //if the game being started is not coinflip, then redirect the user to the error page.
            //This will not be the case in real life as every casino game listed will have a game associated with it
            //but for the sake of demonstration there is multiple games up that have no implementation
            if (gameId != 10)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var verification = _verificationRepository.RetrieveVerificationByUserId(userId);
            if (verification == null ||verification.Status != "Approved" )
            {
                return RedirectToAction("Index", "Verification", new { code = 404 });
            }
            var ownedPasses = _casinoPassRepository.GetCasinoPassesByUserId(userId);
            var gameDetails = _gameRepository.GetGameById(gameId);
            //remove space from game name
            string gameName = Regex.Replace(gameDetails.Name, @"\s+", "");

            //CasinoPass activePass = ownedPasses.FirstOrDefault(p => (p.Active == true) && (p.Expired == false) && (p.GameId == 1));
            CasinoPass activePass = ownedPasses.FirstOrDefault(p => (p.GameId == gameId) && (p.Active == true) && (p.Expired == false));
            //if its active already
            if (activePass != null)
            {
                return RedirectToAction("Index", "Play", new { area = gameName, casinoPassId = activePass.CasinoPassId });
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
