using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class CategoryRepository : ICategoryRepository
	{
		public IEnumerable<Category> GetAllCategories => new List<Category>
		{
			new Category{CategoryId=1,CategoryName="Casino Games",CategoryDescription="Casino games where you bet real money"},
			new Category{CategoryId=2, CategoryName="Browser Games",CategoryDescription="Traditional games played in browser"},
			new Category{CategoryId=3, CategoryName="Download Games",CategoryDescription="Traditional Games that need to be downloaded"}
		};
	}
}
