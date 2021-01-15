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

		public IActionResult List()
		{
			//ViewBag.CurrentCategory = "Bestsellers";
			//return View(_gameRepository.GetAllGames);

			var gameListViewModel = new GameListViewModel();
			gameListViewModel.Games = _gameRepository.GetAllGames;
			gameListViewModel.CurrentCategory = "Best Sellers";
			return View(gameListViewModel);
		}
	}
}
