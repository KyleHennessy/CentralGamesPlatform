﻿using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.ViewModels
{
    public class LibraryViewModel
    {
        public IEnumerable<Game> OwnedGames { get; set; }
    }
}
