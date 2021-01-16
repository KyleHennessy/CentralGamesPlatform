using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;

namespace CentralGamesPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameRepository _gameRepository;

		public HomeController(IGameRepository gameRepository)
		{
            _gameRepository = gameRepository;
		}
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                GamesOnSale = _gameRepository.GetGamesOnSale
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
