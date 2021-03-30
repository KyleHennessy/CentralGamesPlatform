using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.ViewModels
{
    public class LibraryViewModel
    {
        public IEnumerable<Game> OwnedGames { get; set; }
        public IEnumerable<CasinoPass> OwnedPasses { get; set; }
        public IEnumerable<Game> ActiveCasinoGames { get; set; }
    }
}
