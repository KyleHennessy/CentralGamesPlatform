using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.ViewModels
{
    public class AdminDownloadViewModel
    {
        public string FileName { get; set; }
        public double FileVersion { get; set; }
        public DateTime LastUpdated { get; set; }
        public int GameId { get; set; }
    }
}
