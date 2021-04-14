using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.ViewModels
{
	public class GameListViewModel
	{
		public IEnumerable<Game> Games { get; set; }
		public List<int> OwnedGameIds { get; set; }
		public string CurrentCategory { get; set; }
	}
}
