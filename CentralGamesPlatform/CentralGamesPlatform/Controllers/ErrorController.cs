using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    public class ErrorController : Controller
    {
        
        public IActionResult HandleError(int? code)
        {
            if (code == null)
            {
               return RedirectToAction("Index", "Home");
            }
            ViewData["ErrorMessage"] = $"Error code: {code}";
            return View();
        }
    }
}
