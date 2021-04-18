using CentralGamesPlatform.IRepositories;
using CentralGamesPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Controllers
{
    [Authorize]
    public class VerificationController : Controller
    {
        private readonly IVerificationRepository _verificationRepository;
        public VerificationController(IVerificationRepository verificationRepository)
        {
            _verificationRepository = verificationRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(FileUpload fileObject)
        {
            string[] permittedExtensions = { ".png", ".jpg" };
            var uploadedExtension = Path.GetExtension(fileObject.file.FileName).ToLowerInvariant();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var verificationExists = _verificationRepository.RetrieveVerificationByUserId(userId);
            if (verificationExists == null)
            {
                if (fileObject.file.Length > 0 && permittedExtensions.Contains(uploadedExtension))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileObject.file.CopyTo(memoryStream);
                        if (memoryStream.Length < 2097152)
                        {
                            var verificationRequest = new Verification()
                            {
                                Content = memoryStream.ToArray(),
                                UserId = userId,
                                Status = "Pending"
                       
                            };
                            _verificationRepository.CreateVerification(verificationRequest);
                            ViewBag.SuccessMessage = "You have submitted your photo ID for verification. This will be processed by an admin shortly. You will not be able to play casino games until it has been verified";
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too small or is in the wrong format");
                }
            }
            else if (verificationExists.Status == "Approved")
            {
                ViewBag.ErrorMessage = "Your account is already verified";
            }
            else if (verificationExists.Status == "Denied")
            {
                ViewBag.ErrorMessage = "Your photo ID could not be verified. Contact support if you think this is a mistake";
            }
            else if (verificationExists.Status == "Pending")
            {
                ViewBag.ErrorMessage = "Your verification request is currently pending";
            }
            else if(verificationExists.Status == "")
            {
                ViewBag.ErrorMessage = "Something went wrong with your verification. Please contact an admin";
            }

            return View();
        }
    }
}
