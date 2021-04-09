using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyDatabaseContext _myDatabaseContext;
        public AdminController(RoleManager<IdentityRole> roleManager, IGameRepository gameRepository, MyDatabaseContext myDatabaseContext)
        {
            _roleManager = roleManager;
            _gameRepository = gameRepository;
            _myDatabaseContext = myDatabaseContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task <IActionResult> ViewGames(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategoryIdSortParam"] = sortOrder == "CategoryId" ? "categoryid_desc" : "CategoryId";
            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var games = from g in _myDatabaseContext.Games select g;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(g => g.Name);
                    break;
                case "CategoryId":
                    games = games.OrderBy(g => g.CategoryId);
                    break;
                case "categoryid_desc":
                    games = games.OrderByDescending(g => g.CategoryId);
                    break;
                default:
                    games = games.OrderBy(g => g.Name);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public IActionResult Details(int? gameId)
        {
            if (gameId == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            var game = _gameRepository.GetGameById((int)gameId);
            if (game == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            return View(game);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDatabaseContext.Add(game);
                    _myDatabaseContext.SaveChanges();
                    return RedirectToAction("ViewGames");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes to the database");
            }
            return View(game);
        }
        public IActionResult Edit(int? gameId)
        {
            if (gameId == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            var game = _gameRepository.GetGameById((int)gameId);
            if (game == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            return View(game);
        }
        [HttpPost]
        public IActionResult Edit(int gameId, Game game)
        {
            if(gameId != game.GameId)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _myDatabaseContext.Update(game);
                    _myDatabaseContext.SaveChanges();
                    return RedirectToAction("ViewGames");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes to the database");
                }
            }
            return View(game);
        }
        
        //[HttpGet]
        //public IActionResult CreateRole()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // We just need to specify a unique role name to create a new role
        //        IdentityRole identityRole = new IdentityRole
        //        {
        //            Name = model.RoleName
        //        };

        //        // Saves the role in the underlying AspNetRoles table
        //        IdentityResult result = await _roleManager.CreateAsync(identityRole);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }

        //        foreach (IdentityError error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }

        //    return View(model);
        //}
    }
}
