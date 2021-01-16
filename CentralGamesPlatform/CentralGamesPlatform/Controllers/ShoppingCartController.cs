using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly IGameRepository _gameRepository;
		private readonly ShoppingCart _shoppingCart;

		public ShoppingCartController(IGameRepository gameRepository, ShoppingCart shoppingCart)
		{
			_gameRepository = gameRepository;
			_shoppingCart = shoppingCart;
		}

		public ViewResult Index()
		{
			_shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

			var shoppingCartViewModel = new ShoppingCartViewModel
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};

			return View(shoppingCartViewModel);
		}

		public RedirectToActionResult AddToShoppingCart(int gameId)
		{
			var selectedGame = _gameRepository.GetAllGames.FirstOrDefault(g => g.GameId == gameId);

			if (selectedGame != null)
			{
				_shoppingCart.AddToCart(selectedGame,1);
			}

			return RedirectToAction("Index");
		}

		public RedirectToActionResult RemoveFromShoppingCart(int gameId)
		{
			var selectedGame = _gameRepository.GetAllGames.FirstOrDefault(g => g.GameId == gameId);

			if (selectedGame != null)
			{
				_shoppingCart.RemoveFromCart(selectedGame);
			}

			return RedirectToAction("Index");
		}
	}
}
