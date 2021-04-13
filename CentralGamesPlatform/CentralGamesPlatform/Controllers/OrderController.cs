using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Security.Claims;

namespace CentralGamesPlatform.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ICasinoPassRepository _casinoPassRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ShoppingCart _shoppingCart;

		public OrderController(IOrderRepository orderRepository, ICasinoPassRepository casinoPassRepository,
							   ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager )
		{
			_orderRepository = orderRepository;
			_casinoPassRepository = casinoPassRepository;
			_shoppingCart = shoppingCart;
			_userManager = userManager;	
		}

		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			decimal total;
			_shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
			total = _shoppingCart.GetShoppingCartTotal();

			if(_shoppingCart.ShoppingCartItems.Count == 0)
			{
				ModelState.AddModelError("", "Your cart is empty");
			}

			if (ModelState.IsValid)
			{
				string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				order.UserId = userId;
				_orderRepository.CreateOrder(order);
				TempData["orderId"] = order.OrderId;
				return RedirectToAction("Index", "Payment");
			}
			
			return View(order);
		}

		//public IActionResult CheckoutComplete()
		//{
		//	ViewBag.CheckoutCompleteMessage = "Thank you for your order. Your purchased games are available from your account immediately";
		//	return View();
		//}
	}
}
