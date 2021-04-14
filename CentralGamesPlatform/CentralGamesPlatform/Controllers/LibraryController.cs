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
        private readonly MyDatabaseContext _myDatabaseContext;
        public LibraryController(IGameRepository gameRepository, ILicenceRepository licenceRepository, 
                                 IOrderDetailRepository orderDetailRepository, ICasinoPassRepository casinoPassRepository,
                                 MyDatabaseContext myDatabaseContext)
        {
            _gameRepository = gameRepository;
            _licenceRepository = licenceRepository;
            _orderDetailRepository = orderDetailRepository;
            _casinoPassRepository = casinoPassRepository;
            _myDatabaseContext = myDatabaseContext;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var licences = _licenceRepository.GetLicences(userId);
            var query = (from l in _myDatabaseContext.Licences
                         where l.UserId == userId
                         join od in _myDatabaseContext.OrderDetails on l.OrderDetailId equals od.OrderDetailId
                         join g in _myDatabaseContext.Games on od.GameId equals g.GameId
                         select g).ToList();
            query.RemoveAll(og => og.GameId == -1);
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
                OwnedGames = query,
                OwnedPasses = ownedPasses,
                ActiveCasinoGames = activeCasinoGames
            };
            return View(libraryViewModel);
        }
    }
}
