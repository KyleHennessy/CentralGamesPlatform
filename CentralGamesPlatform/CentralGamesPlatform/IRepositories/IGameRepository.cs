﻿using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.IRepositories
{
	public interface IGameRepository
	{
		IEnumerable<Game> GetAllGames { get; }
		IEnumerable<Game> GetGamesOnSale { get; }
		Game GetGameById(int gameId);
	}
}
