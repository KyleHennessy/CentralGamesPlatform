using CentralGamesPlatform.Models;
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

		public ViewResult List()
		{
			return View(_gameRepository.GetAllGames);
		}
	}
}
