using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
	public class ShoppingCartController : Controller
    {
		private readonly IGameRepository _gameRepository;
		private readonly ShoppingCart _shoppingCart;
		private readonly MyDatabaseContext _myDatabaseContext;

		public ShoppingCartController(IGameRepository gameRepository, ShoppingCart shoppingCart,
									  MyDatabaseContext myDatabaseContext)
		{
			_gameRepository = gameRepository;
			_shoppingCart = shoppingCart;
			_myDatabaseContext = myDatabaseContext;
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

		[Authorize]
		public RedirectToActionResult AddToShoppingCart(int gameId)
		{
			string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
			if(shoppingCartItems == null)
            {
				Response.StatusCode = 404;
				return RedirectToAction("HandleError", "Error", new { code = 404 });
			}
			var query = (from l in _myDatabaseContext.Licences
						 where l.UserId == userId
						 join od in _myDatabaseContext.OrderDetails on l.OrderDetailId equals od.OrderDetailId
						 join g in _myDatabaseContext.Games on od.GameId equals g.GameId
						 select g.GameId).ToList();
			foreach(var item in shoppingCartItems)
            {
				if (item.Game.GameId == gameId && gameId != -1)
                {
					return RedirectToAction("Index");
				}
            } 
			if(gameId == -1 || !query.Contains(gameId))
            {
				var selectedGame = _gameRepository.GetAllGames.FirstOrDefault(g => g.GameId == gameId);

				if (selectedGame != null)
				{
					_shoppingCart.AddToCart(selectedGame, 1);
				}

				return RedirectToAction("Index");
			}
            else
            {
				return RedirectToAction("Index", "Library");
			}
			
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
