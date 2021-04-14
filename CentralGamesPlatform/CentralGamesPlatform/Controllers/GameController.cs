using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
	public class GameController : Controller
	{
		private readonly IGameRepository _gameRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly MyDatabaseContext _myDatabaseContext;

		public GameController(IGameRepository gameRepository, ICategoryRepository categoryRepository,
							  MyDatabaseContext myDatabaseContext)
		{
			_gameRepository = gameRepository;
			_categoryRepository = categoryRepository;
			_myDatabaseContext = myDatabaseContext;
		}

		public ViewResult List(string category)
		{
			string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var query = (from l in _myDatabaseContext.Licences
						 where l.UserId == userId
						 join od in _myDatabaseContext.OrderDetails on l.OrderDetailId equals od.OrderDetailId
						 join g in _myDatabaseContext.Games on od.GameId equals g.GameId
						 select g.GameId).ToList();
			IEnumerable <Game> games;
			//IEnumerable ownedGameIds = query;
			string currentCategory;
			//if category is null then current category is all games
			if (string.IsNullOrEmpty(category))
			{
				games = _gameRepository.GetAllGames.OrderBy(g => g.GameId);
				currentCategory = "All Games";
			}
			else
			{
				//find games that matches category name argument
				games = _gameRepository.GetAllGames.Where(c => c.Category.CategoryName == category);
				currentCategory = _categoryRepository.GetAllCategories.FirstOrDefault(currentCategory => currentCategory.CategoryName == category)?.CategoryName;		
			}

			return View(new GameListViewModel
			{
				Games = games,
				OwnedGameIds = query,
				CurrentCategory = currentCategory
			});
		}

		public IActionResult Details(int id)
		{
			var game = _gameRepository.GetGameById(id);
			if (game == null)
				return NotFound();

			return View(game);

		}
	}
}
