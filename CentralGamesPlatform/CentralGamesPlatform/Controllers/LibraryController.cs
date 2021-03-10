using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using CentralGamesPlatform.ViewModels;

namespace CentralGamesPlatform.Controllers
{
	public class LibraryController : Controller
	{
        private readonly IGameRepository _gameRepository;
        private readonly ILicenceRepository _licenceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public LibraryController(IGameRepository gameRepository, ILicenceRepository licenceRepository, 
                                 IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _gameRepository = gameRepository;
            _licenceRepository = licenceRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var licences = _licenceRepository.GetLicences(userId);
            List<int> orderDetailIds = new List<int>();
            foreach (var licence in licences)
            {
                orderDetailIds.Add(licence.OrderDetailId);
            }
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            for(int i = 0; i < orderDetailIds.Count(); i++)
            {
                //int id = orderDetailIds[i];
                orderDetails.Add(_orderDetailRepository.GetOrderDetailById(orderDetailIds[i]));
            }
            List<Game> ownedGames = new List<Game>();
            foreach (var orderDetail in orderDetails)
            {
                ownedGames.Add(_gameRepository.GetGameById(orderDetail.GameId));
            }
            var libraryViewModel = new LibraryViewModel
            {
                OwnedGames = ownedGames
            };
            return View(libraryViewModel);
        }
    }
}
