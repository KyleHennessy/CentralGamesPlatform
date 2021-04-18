using CentralGamesPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.IRepositories
{
    public interface IVerificationRepository
    {
        void CreateVerification(Verification verification);
        void UpdateVerification(int verificationId, string status);
        IEnumerable<Verification> RetrieveAllVerifications();
        Verification RetrieveVerificationById(int verificationId);
        Verification RetrieveVerificationByUserId(string userId);

    }
}
