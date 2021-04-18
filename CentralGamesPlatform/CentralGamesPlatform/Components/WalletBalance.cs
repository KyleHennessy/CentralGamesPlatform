using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
using CentralGamesPlatform.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Components
{
    public class WalletBalance : ViewComponent
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WalletBalance(IWalletRepository walletRepository, IHttpContextAccessor httpContextAccessor)
        {
            _walletRepository = walletRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            decimal balance = _walletRepository.RetrieveBalance(userId);
            var walletViewModel = new WalletViewModel
            {
                WalletBalance = balance
            };
            return View(walletViewModel);
        }
    }
}
