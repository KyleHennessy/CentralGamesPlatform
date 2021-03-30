using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
	public class GameController : Controller
	{
		private readonly IGameRepository _gameRepository;
		private readonly ICategoryRepository _categoryRepository;

		public GameController(IGameRepository gameRepository, ICategoryRepository categoryRepository)
		{
			_gameRepository = gameRepository;
			_categoryRepository = categoryRepository;
		}

		public ViewResult List(string category)
		{
			IEnumerable<Game> games;
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
