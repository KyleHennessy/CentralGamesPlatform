using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Areas.CoinFlip.Controllers
{
    [Authorize]
    [Area("CoinFlip")]
    public class PlayController : Controller
    {
        private readonly ICasinoPassRepository _casinoPassRepository;
        private readonly IResultRepository _resultRepostiory;
        private readonly IWalletRepository _walletRepository;
        public PlayController(ICasinoPassRepository casinoPassRepository, IResultRepository resultRepository,
                              IWalletRepository walletRepository)
        {
            _casinoPassRepository = casinoPassRepository;
            _resultRepostiory = resultRepository;
            _walletRepository = walletRepository;
        }
        public IActionResult Index(Guid casinoPassId)
        {
            var pass = _casinoPassRepository.GetCasinoPass(casinoPassId);
            //If pass is active and game id is the same as this game
            if(pass == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (pass.Active == true && pass.GameId == 10)
            { 
                return View(pass);
            }
            else
            {
                return RedirectToAction("Index", "Library", new { area = "" });
            }
        }

        public IActionResult Decide(Guid casinoPassId)
        {
            var casinoPass = _casinoPassRepository.GetCasinoPass(casinoPassId);
            if(casinoPass == null) 
            {
                return RedirectToAction("Index", "Home", new { area = "" });  
            }
            if(casinoPass.Expired == true && casinoPass.Active == false)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            Random rand = new Random();
            if (rand.Next(0, 2) == 0)
            {

                Result result = new Result();
                result.Win = false;
                result.CasinoPassId = casinoPass.CasinoPassId;
                result.AmountWon = 0.00M;
                if (casinoPass.Expired == false && casinoPass.Active == true && casinoPass.GameId == 10)
                {
                    _resultRepostiory.CreateResult(result);
                    _casinoPassRepository.ExpireCasinoPass(casinoPass.CasinoPassId);
                    
                }
                else
                {
                    RedirectToAction("Index", "Home", new { area = "" });
                }
                return View("Loss");
            }
            else
            {
                Result result = new Result();
                result.Win = true;
                result.CasinoPassId = casinoPass.CasinoPassId;
                result.AmountWon = 10.00M;
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                
                
                if (casinoPass.Expired == false && casinoPass.Active == true && casinoPass.GameId == 10)
                {
                    _resultRepostiory.CreateResult(result);
                    _walletRepository.AddToWallet(userId, result.AmountWon);
                    _casinoPassRepository.ExpireCasinoPass(casinoPass.CasinoPassId);
                    
                }
                else
                {
                    RedirectToAction("Index", "Home", new { area = "" });
                }
                return View("Win");
            }
        }
    }
}
