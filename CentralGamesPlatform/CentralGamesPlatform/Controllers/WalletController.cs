using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletRepository _walletRepository;
        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }
        public ViewResult Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            decimal balance = _walletRepository.RetrieveBalance(userId);
            
            var walletViewModel = new WalletViewModel
            {
                WalletBalance = balance
            };
            return View(walletViewModel);
        }
    }
}
