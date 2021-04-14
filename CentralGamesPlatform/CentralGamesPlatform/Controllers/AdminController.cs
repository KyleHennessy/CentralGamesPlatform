using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IVerificationRepository _verificationRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyDatabaseContext _myDatabaseContext;
        public AdminController(RoleManager<IdentityRole> roleManager, IGameRepository gameRepository, 
                               MyDatabaseContext myDatabaseContext, IVerificationRepository verificationRepository)
        {
            _roleManager = roleManager;
            _gameRepository = gameRepository;
            _verificationRepository = verificationRepository;
            _myDatabaseContext = myDatabaseContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Manage Games
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
            int pageSize = 10;
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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? gameId)
        {
            if (gameId == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            var gameToUpdate = await _myDatabaseContext.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
            if (await TryUpdateModelAsync<Game>(
                gameToUpdate,
                "",
                g => g.Name, g => g.Description, g => g.Price, g => g.ImageUrl, g => g.ImageThumbnailUrl, g => g.IsOnSale, g => g.CategoryId))
            {
                try
                {
                    await _myDatabaseContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes to the database");
                }
            }
            return View(gameToUpdate);
        }

        public async Task<IActionResult> ViewVerificationRequests(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["StatusSortParam"] = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewData["DateOfRequestSortParam"] = sortOrder == "DateOfRequest" ? "dateofrequest_desc" : "DateOfRequest";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var verifications = from v in _myDatabaseContext.Verifications select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                verifications = verifications.Where(v => v.UserId.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "status_desc":
                    verifications = verifications.OrderByDescending(v => v.Status);
                    break;
                case "DateOfRequest":
                    verifications = verifications.OrderBy(v => v.DateOfRequest);
                    break;
                case "dateofrequest_desc":
                    verifications = verifications.OrderByDescending(v => v.DateOfRequest);
                    break;
                default:
                    verifications = verifications.OrderBy(v => v.DateOfRequest);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Verification>.CreateAsync(verifications.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public IActionResult UpdateVerificationRequest(int? verificationId)
        {
            if (verificationId == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            var verificationRequest = _verificationRepository.RetrieveVerificationById((int)verificationId);
            if (verificationRequest == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            using (MemoryStream memoryStream = new MemoryStream(verificationRequest.Content))
            {
                ViewData["Image"] = Convert.ToBase64String(verificationRequest.Content);
            }
            
            return View(verificationRequest);
        }

        [HttpPost, ActionName("UpdateVerificationRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVerificationRequestPost(int? verificationId)
        {
            if (verificationId == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            var verificationRequestToUpdate = await _myDatabaseContext.Verifications.FirstOrDefaultAsync(g => g.VerificationId == verificationId);
            if (await TryUpdateModelAsync<Verification>(
                verificationRequestToUpdate,
                "",
                v => v.Status))
            {
                try
                {
                    await _myDatabaseContext.SaveChangesAsync();
                    return RedirectToAction("ViewVerificationRequests");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes to the database");
                }
            }
            return View(verificationRequestToUpdate);
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
