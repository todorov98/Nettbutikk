using Microsoft.AspNetCore.Identity;
using System;

namespace Nettbutikk.Models
{
    public class TokenEntity : IdentityUserToken<string>
    {
        public DateTime ValidTo { get; set; }
        public DateTime ValidFrom { get; set; }
        public bool LogoutTerminated { get; set; }
        public DateTime LogoutTerminationDate { get; set; }
        public string SignatureAlgorithm { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
