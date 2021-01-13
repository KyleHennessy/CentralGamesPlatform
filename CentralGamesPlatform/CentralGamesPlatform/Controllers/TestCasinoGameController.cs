using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CentralGamesPlatform.Controllers
{
    public class TestCasinoGameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Decide()
        {
            Random rand = new Random();
            if (rand.Next(0, 2) == 0)
            {
                //Record loss in database
                return View("Loss");
            }
            else
            {
                //Record win in database and credit money to user's account
                return View("Win");
            }
        }
    }
}
