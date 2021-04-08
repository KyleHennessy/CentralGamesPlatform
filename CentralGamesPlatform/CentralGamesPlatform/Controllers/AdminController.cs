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
        public async Task <IActionResult> ViewGames(string sortOrder)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategoryIdSortParm"] = sortOrder == "CategoryId" ? "categoryid_desc" : "CategoryId";
            //var games = _gameRepository.GetAllGames.OrderBy(g => g.GameId);
            var games = from g in _myDatabaseContext.Games select g;
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
            return View(await games.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
