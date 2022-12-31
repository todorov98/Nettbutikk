using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikk.Models
{
    public class UserRoleRelationEntity : IdentityUserRole<string>
    {
        [Key]
        public override string UserId { get => base.UserId; set => base.UserId = value; }
        [Key]
        public override string RoleId { get => base.RoleId; set => base.RoleId = value; }
    }
}
