using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Repositories
{
    public class DownloadRepository : IDownloadRepository
    {
        private readonly MyDatabaseContext _myDatabaseContext;
        public DownloadRepository(MyDatabaseContext myDatabaseContext)
        {
            _myDatabaseContext = myDatabaseContext;
        }
        public void CreateDownload(Download download)
        {
            download.LastUpdated = DateTime.Now;
            _myDatabaseContext.Downloads.Add(download);
            _myDatabaseContext.SaveChanges();
        }

        public string GetDownloadFileName(int gameId)
        {
            var result = (from d in _myDatabaseContext.Downloads
                          where d.GameId == gameId
                          select d.FileName).SingleOrDefault();
            return result;
        }

        public DateTime GetDownloadLastUpdated(int gameId)
        {
            var result = (from d in _myDatabaseContext.Downloads
                          where d.GameId == gameId
                          select d.LastUpdated).SingleOrDefault();
            return result;
        }

        public double GetDownloadVersion(int gameId)
        {
            var result = (from d in _myDatabaseContext.Downloads
                          where d.GameId == gameId
                          select d.VersionNumber).SingleOrDefault();
            return result;
        }

        public Download RetrieveDownloadByGameId(int gameId)
        {
            var result = (from d in _myDatabaseContext.Downloads
                          where d.GameId == gameId
                          select d).SingleOrDefault();
            return result;
        }

        public Download RetrieveDownloadById(int downloadId)
        {
            var result = (from d in _myDatabaseContext.Downloads
                          where d.DownloadId == downloadId
                          select d).SingleOrDefault();
            return result;
        }

        public void UpdateDownload(Download download)
        {
            var result = (from d in _myDatabaseContext.Downloads
                          where d.DownloadId == download.DownloadId
                          select d).SingleOrDefault();
            result.Content = download.Content;
            result.VersionNumber = download.VersionNumber;
            result.LastUpdated = DateTime.Now;

            _myDatabaseContext.SaveChanges();
        }
    }
}
