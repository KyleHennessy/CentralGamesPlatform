using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class GameRepository : IGameRepository
	{
		private readonly ICategoryRepository _categoryRepository = new CategoryRepository();
		public IEnumerable<Game> GetAllGames => new List<Game> { 
			new Game {GameId=1,Name="Poker", Price=5.00M, Description="Card Game of Poker", Category = _categoryRepository.GetAllCategories.ToList()[0],
				ImageUrl="https://specials-images.forbesimg.com/imageserve/5d7b1e04aea4d30008f0d674/960x0.jpg?fit=scale", IsOnSale=false,
				ImageThumbnailUrl="https://is3-ssl.mzstatic.com/image/thumb/Purple124/v4/98/6a/2d/986a2d9d-d1e9-cbc4-4782-634c1eb48139/source/256x256bb.jpg"},
			new Game {GameId=2,Name="Happy Wheels", Price=10.00M,Description="Bike game that is played in browser", Category = _categoryRepository.GetAllCategories.ToList()[1],
				ImageUrl="https://media.distractify.com/brand-img/Dd48wzkUJ/0x0/happy-wheels-cover-1578065247154.jpg", IsOnSale=true,
				ImageThumbnailUrl="https://media.abcya3.net/images/300/happy-wheels-2.jpg"},
			new Game {GameId=3,Name="Call of Duty", Price=50.00M,Description="Call of duty game that needs to be downloaded", Category = _categoryRepository.GetAllCategories.ToList()[2],
				ImageUrl="https://www.paysafecard.com/fileadmin/Website/News/News_Area/Games/modernwarfare.jpg", IsOnSale=true,
				ImageThumbnailUrl="https://www.trukocash.com/img/games/75_logo_name.png"}
		};

		public IEnumerable<Game> GetGamesOnSale => throw new NotImplementedException();

		public Game GetGameById(int gameId)
		{
			return GetAllGames.FirstOrDefault(g => g.GameId == gameId);
		}
	}
}
