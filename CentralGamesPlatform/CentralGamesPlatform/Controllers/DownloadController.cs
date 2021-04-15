using CentralGamesPlatform.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IDownloadRepository _downloadRepository;
        public DownloadController(IDownloadRepository downloadRepository)
        {
            _downloadRepository = downloadRepository;
        }
        public FileResult DownloadGame(int gameId)
        {
            var download = _downloadRepository.RetrieveDownloadByGameId(gameId);
            return File(download.Content, "application/force-download", download.FileName);
        }
    }
}
