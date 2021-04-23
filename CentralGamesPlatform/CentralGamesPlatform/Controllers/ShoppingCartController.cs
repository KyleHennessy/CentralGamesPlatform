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
	[Authorize]
	public class ShoppingCartController : Controller
    {
		private readonly IGameRepository _gameRepository;
		private readonly ShoppingCart _shoppingCart;
		private readonly MyDatabaseContext _myDatabaseContext;
		private readonly ICasinoPassRepository _casinoPassRepository;

		public ShoppingCartController(IGameRepository gameRepository, ShoppingCart shoppingCart,
									  MyDatabaseContext myDatabaseContext, ICasinoPassRepository casinoPassRepository)
		{
			_gameRepository = gameRepository;
			_shoppingCart = shoppingCart;
			_myDatabaseContext = myDatabaseContext;
			_casinoPassRepository = casinoPassRepository;
		}

		public IActionResult Index()
		{
			_shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
			if(_shoppingCart.ShoppingCartItems.Count == 0)
            {
				return RedirectToAction("Index", "Home");
            }

			var shoppingCartViewModel = new ShoppingCartViewModel
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};

			return View(shoppingCartViewModel);
		}

		public RedirectToActionResult AddToShoppingCart(int gameId)
		{
			string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
			if(shoppingCartItems == null)
            {
				Response.StatusCode = 404;
				return RedirectToAction("HandleError", "Error", new { code = 404 });
			}
			if(gameId == -1)
            {
				int amountPurchasedToday = _casinoPassRepository.AmountPurchasedToday(userId);
				//var count = shoppingCartItems.Where(item => item.Game.GameId == -1).Count();
				var count = shoppingCartItems.Where(g => g.Game.GameId == -1).Select(c => c.Amount).FirstOrDefault();
				if (amountPurchasedToday >= 10 || (count >= 10 || count >= (10 - amountPurchasedToday)))
                {
					TempData["ErrorMessage"] = "You have purchased too many casino passes today. This platform does not enable or encourage impulsive gambling in any way. Consider looking at some video games we have on offer for a safe and fun outlet!";
					_shoppingCart.ClearCart();
					return RedirectToAction("Index", "Library");
                }
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
				ViewBag.ErrorMessage = "You already own this game";
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

		public RedirectToActionResult ClearCart()
        {
			_shoppingCart.ClearCart();
			return RedirectToAction("Index");
        }
	}
}
