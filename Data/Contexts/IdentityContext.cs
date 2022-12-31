using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nettbutikk.Models;

namespace Nettbutikk.Data
{
    public class IdentityContext : IdentityDbContext
    {
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<RoleEntity> RoleEntities { get; set; }
        public DbSet<UserRoleRelationEntity> UserRoleRelations { get; set; }
        public DbSet<TokenEntity> StoredUserTokens { get; set; }
        public DbSet<IdentityUserClaim<string>> StoredUserClaims { get; set; }
        public DbSet<IdentityRoleClaim<string>> StoredRoleClaims { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
