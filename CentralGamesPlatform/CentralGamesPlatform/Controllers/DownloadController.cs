using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
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
    public class DownloadController : Controller
    {
        private readonly IDownloadRepository _downloadRepository;
        private readonly MyDatabaseContext _myDatabaseContext;
        public DownloadController(IDownloadRepository downloadRepository, MyDatabaseContext myDatabaseContext)
        {
            _downloadRepository = downloadRepository;
            _myDatabaseContext = myDatabaseContext;
        }
        public FileResult DownloadGame(int? gameId)
        {
            if (gameId == null)
            {
                return null;
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
                return null;
            }
            var download = _downloadRepository.RetrieveDownloadByGameId((int)gameId);
            return File(download.Content, "application/force-download", download.FileName + download.FileFormat);
        }
    }
}
