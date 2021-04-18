using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    [Authorize]
    public class DownloadController : Controller
    {
        private readonly IDownloadRepository _downloadRepository;
        private readonly MyDatabaseContext _myDatabaseContext;
        public DownloadController(IDownloadRepository downloadRepository, MyDatabaseContext myDatabaseContext)
        {
            _downloadRepository = downloadRepository;
            _myDatabaseContext = myDatabaseContext;
        }
        public IActionResult DownloadGame(int? gameId)
        {
            if (gameId == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var query = (from l in _myDatabaseContext.Licences
                         where l.UserId == userId
                         join od in _myDatabaseContext.OrderDetails on l.OrderDetailId equals od.OrderDetailId
                         join g in _myDatabaseContext.Games on od.GameId equals g.GameId
                         where g.GameId == gameId
                         select g).SingleOrDefault();
            if (query == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            var download = _downloadRepository.RetrieveDownloadByGameId((int)gameId);
            if (download == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            return File(download.Content, "application/force-download", download.FileName + download.FileFormat);
        }
        public IActionResult PlayGame(int? gameId)
        {
            if (gameId == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var query = (from l in _myDatabaseContext.Licences
                         where l.UserId == userId
                         join od in _myDatabaseContext.OrderDetails on l.OrderDetailId equals od.OrderDetailId
                         join g in _myDatabaseContext.Games on od.GameId equals g.GameId
                         where g.GameId == gameId
                         select g).SingleOrDefault();
            if (query == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            var download = _downloadRepository.RetrieveDownloadByGameId((int)gameId);
            if(download == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("HandleError", "Error", new { code = 404 });
            }
            using (MemoryStream memoryStream = new MemoryStream(download.Content))
            {
                ViewData["game"] = Convert.ToBase64String(download.Content);
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
