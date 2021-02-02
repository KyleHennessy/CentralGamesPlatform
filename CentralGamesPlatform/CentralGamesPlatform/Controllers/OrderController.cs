using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ShoppingCart _shoppingCart;

		public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
		{
			_orderRepository = orderRepository;
			_shoppingCart = shoppingCart;
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
				_orderRepository.CreateOrder(order);
				TempData["orderId"] = order.OrderId;
				return RedirectToAction("Index", "Payment");
				//return RedirectToAction("CheckoutComplete");
			}

			return View(order);
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Thank you for your order. Your purchased games are available from your account immediately";
			return View();
		}
	}
}
