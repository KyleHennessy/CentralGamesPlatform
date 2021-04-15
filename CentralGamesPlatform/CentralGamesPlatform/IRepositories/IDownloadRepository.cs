using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.IRepositories
{
    public interface IDownloadRepository
    {
        void CreateDownload(Download download);
        void UpdateDownload(Download download);
        Download RetrieveDownloadById(int gameId);
        Download RetrieveDownloadByGameId(int gameId);
        string GetDownloadFileName(int gameId);
        double GetDownloadVersion(int gameId);
        DateTime GetDownloadLastUpdated(int gameId);
    }
}
