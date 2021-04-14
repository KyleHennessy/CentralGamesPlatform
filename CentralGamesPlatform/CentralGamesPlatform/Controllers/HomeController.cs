using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CentralGamesPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly MyDatabaseContext _myDatabaseContext;

		public HomeController(IGameRepository gameRepository, MyDatabaseContext myDatabaseContext)
		{
            _gameRepository = gameRepository;
            _myDatabaseContext = myDatabaseContext;
		}
        public IActionResult Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var query = (from l in _myDatabaseContext.Licences
                         where l.UserId == userId
                         join od in _myDatabaseContext.OrderDetails on l.OrderDetailId equals od.OrderDetailId
                         join g in _myDatabaseContext.Games on od.GameId equals g.GameId
                         select g.GameId).ToList();
            var homeViewModel = new HomeViewModel
            {
                GamesOnSale = _gameRepository.GetGamesOnSale,
                OwnedGameIds = query
            };
            return View(homeViewModel);
        }
        public async Task <IActionResult> Search(string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var games = from g in _myDatabaseContext.Games select g;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Name.Contains(searchString));
            }
            else
            {
                games = games.OrderBy(g => g.Name);
            }
            int pageSize = 5;
            return View(await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), pageNumber ?? 1, pageSize));
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
