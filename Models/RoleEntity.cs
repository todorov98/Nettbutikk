using Microsoft.AspNetCore.Identity;

namespace Nettbutikk.Models
{
    public class RoleEntity : IdentityRole
    {
        public string Code { get; set; }
    }
}
