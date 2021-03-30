using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    [Authorize]
    public class StartController : Controller
    {
        public StartController()
        {

        }


        
        public RedirectToActionResult GiveAccessToCasinoGame(int gameId)
        {

            return RedirectToAction("CoinFlip", "Index");
        }
    }
}
