using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly MyDatabaseContext _myDatabaseContext;
		public CategoryRepository(MyDatabaseContext myDatabaseContext)
		{
			_myDatabaseContext = myDatabaseContext;
		}
		public IEnumerable<Category> GetAllCategories => _myDatabaseContext.Categories;
	}
}
