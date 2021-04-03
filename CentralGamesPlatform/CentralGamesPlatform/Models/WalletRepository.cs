using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class WalletRepository : IWalletRepository
    {
        private readonly MyDatabaseContext _myDatabaseContext;
        public WalletRepository(MyDatabaseContext myDatabaseContext)
        {
            _myDatabaseContext = myDatabaseContext;
        }
        public void AddToWallet(string userId, decimal amountToAdd)
        {
            Wallet wallet = (from w in _myDatabaseContext.Wallets
                             where w.UserId == userId
                             select w).SingleOrDefault();
            if (wallet == null)
            {
                Wallet newWallet = new Wallet();
                newWallet.UserId = userId;
                newWallet.Balance = 0.00M;
                _myDatabaseContext.Wallets.Add(newWallet);
            }
            decimal result = wallet.Balance + amountToAdd;

        }

        public decimal RetrieveBalance(string userId)
        {
            throw new NotImplementedException();
        }

        public void SubtractFromWallet(string userId, decimal amountToSubtract)
        {
            throw new NotImplementedException();
        }
    }
}
