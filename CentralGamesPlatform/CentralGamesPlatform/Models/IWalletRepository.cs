﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public interface IWalletRepository
    {
        void AddToWallet(string userId, decimal amountToAdd);
        void SubtractFromWallet(string userId, decimal amountToSubtract);
        decimal RetrieveBalance(string userId);
    }
}
