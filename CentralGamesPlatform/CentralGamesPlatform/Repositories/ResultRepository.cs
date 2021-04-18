using CentralGamesPlatform.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class ResultRepository : IResultRepository
    {
        private readonly MyDatabaseContext _myDatabaseContext;
        public ResultRepository(MyDatabaseContext myDatabaseContext)
        {
            _myDatabaseContext = myDatabaseContext;
        }
        public void CreateResult(Result result)
        {
            result.DateResultPlaced = DateTime.Now;
            _myDatabaseContext.Results.Add(result);
            _myDatabaseContext.SaveChanges();
        }
    }
}
