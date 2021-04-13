using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class Verification
    {
        [BindNever]
        public int VerificationId { get; set; }
        public string UserId { get; set; }
        public byte[] Content { get; set; }
        public string Status { get; set; }
        public DateTime DateOfRequest { get; set; }
    }
}
