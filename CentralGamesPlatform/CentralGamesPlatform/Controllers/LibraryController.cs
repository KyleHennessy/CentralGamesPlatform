using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CentralGamesPlatform.Controllers
{
    [Authorize]
	public class LibraryController : Controller
	{
        private readonly IGameRepository _gameRepository;
        private readonly ILicenceRepository _licenceRepository;
        
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICasinoPassRepository _casinoPassRepository;
        public LibraryController(IGameRepository gameRepository, ILicenceRepository licenceRepository, 
                                 IOrderDetailRepository orderDetailRepository, ICasinoPassRepository casinoPassRepository)
        {
            _gameRepository = gameRepository;
            _licenceRepository = licenceRepository;
            _orderDetailRepository = orderDetailRepository;
            _casinoPassRepository = casinoPassRepository;
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
                orderDetails.Add(_orderDetailRepository.GetOrderDetailById(orderDetailIds[i]));
            }
            List<Game> ownedGames = new List<Game>();
            foreach (var orderDetail in orderDetails)
            {
                ownedGames.Add(_gameRepository.GetGameById(orderDetail.GameId));
            }
            ownedGames.RemoveAll(og => og.GameId == -1);
            var usersPasses = _casinoPassRepository.GetCasinoPassesByUserId(userId);
            List<CasinoPass> ownedPasses = new List<CasinoPass>();
            List<Game> activeCasinoGames = new List<Game>();
            foreach(var pass in usersPasses)
            {
                if(pass.Active == false && pass.Expired == false)
                {
                    ownedPasses.Add(pass);
                }
                else if(pass.Active == true && pass.Expired == false)
                {
                    activeCasinoGames.Add(_gameRepository.GetGameById(pass.GameId));
                }
            }
            var libraryViewModel = new LibraryViewModel
            {
                OwnedGames = ownedGames,
                OwnedPasses = ownedPasses,
                ActiveCasinoGames = activeCasinoGames
            };
            return View(libraryViewModel);
        }
    }
}
