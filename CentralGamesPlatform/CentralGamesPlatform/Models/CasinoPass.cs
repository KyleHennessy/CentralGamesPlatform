using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
    public class CasinoPass
    {
        [BindNever]
        public Guid CasinoPassId { get; set; }
        public string UserId { get; set; }
        public DateTime PassPlaced { get; set; }
        public bool Active { get; set; }
        public int GameId { get; set; }
        public bool Expired { get; set; }

    }
}
