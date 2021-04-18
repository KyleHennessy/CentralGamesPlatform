using CentralGamesPlatform.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class VerificationRepository : IVerificationRepository
    {
        private readonly MyDatabaseContext _myDatabaseContext;
        public VerificationRepository(MyDatabaseContext myDatabaseContext)
        {
            _myDatabaseContext = myDatabaseContext;
        }
        public void CreateVerification(Verification verification)
        {
            verification.DateOfRequest = DateTime.Now;
            _myDatabaseContext.Verifications.Add(verification);
            _myDatabaseContext.SaveChanges();
        }

        public IEnumerable<Verification> RetrieveAllVerifications()
        {
            var result = (from v in _myDatabaseContext.Verifications
                                 select v).ToList();
            return result;
        }

        public Verification RetrieveVerificationById(int verificationId)
        {
            var result = (from v in _myDatabaseContext.Verifications
                                where v.VerificationId == verificationId
                                select v).SingleOrDefault();
            return result;
        }

        public Verification RetrieveVerificationByUserId(string userId)
        {
            var result = (from v in _myDatabaseContext.Verifications
                                where v.UserId == userId
                                select v).SingleOrDefault();
            return result;
        }

        public void UpdateVerification(int verificationId, string status)
        {
            var result = (from v in _myDatabaseContext.Verifications
                                where v.VerificationId == verificationId
                                select v).SingleOrDefault();

            result.Status = status;

            _myDatabaseContext.SaveChanges();
        }
    }
}
