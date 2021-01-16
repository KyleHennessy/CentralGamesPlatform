using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class GameRepository : IGameRepository
	{
		private readonly MyDatabaseContext _myDatabaseContext;
		public GameRepository (MyDatabaseContext myDatabaseContext)
		{
			_myDatabaseContext = myDatabaseContext;
		}
		public IEnumerable<Game> GetAllGames 
		{ 
			get
			{
				return _myDatabaseContext.Games.Include(g => g.Category);
			}		
		}

		public IEnumerable<Game> GetGamesOnSale
		{
			get
			{
				return _myDatabaseContext.Games.Include(g => g.Category).Where(s => s.IsOnSale);
			}
		}

		public Game GetGameById(int gameId)
		{
			return _myDatabaseContext.Games.FirstOrDefault(g => g.GameId == gameId);
		}
	}
}
