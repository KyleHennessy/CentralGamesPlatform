using CentralGamesPlatform.IRepositories;
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
                decimal result = newWallet.Balance + amountToAdd;
                newWallet.Balance = result;
                _myDatabaseContext.SaveChanges();
            }
            else
            {
                decimal result = wallet.Balance + amountToAdd;
                wallet.Balance = result;
                _myDatabaseContext.SaveChanges();
            }

        }

        public decimal RetrieveBalance(string userId)
        {
            Wallet wallet = (from w in _myDatabaseContext.Wallets
                             where w.UserId == userId
                             select w).SingleOrDefault();
            if (wallet == null)
            {
                return 0.00M;
            }
            decimal result = wallet.Balance;
            return result;
        }

        public int RetrieveWalletId(string userId)
        {
            Wallet wallet = (from w in _myDatabaseContext.Wallets
                             where w.UserId == userId
                             select w).SingleOrDefault();
            if (wallet == null)
            {
                return 0;
            }
            int result = wallet.WalletId;
            return result;
        }

        public void SubtractFromWallet(string userId, decimal amountToSubtract)
        {
            Wallet wallet = (from w in _myDatabaseContext.Wallets
                             where w.UserId == userId
                             select w).SingleOrDefault();
 
            decimal result = wallet.Balance - amountToSubtract;
            wallet.Balance = result;
            _myDatabaseContext.SaveChanges();
        }
    }
}
