using Microsoft.AspNetCore.Http;
using Nettbutikk.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    public class UserContextService
    {
        private readonly IdentityService _identityService;
        public UserContextService(IdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserEntity> GetCurrentUserOnHttpContext(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;

            if (identity is not null)
            {
                var userClaims = identity.Claims;

                var username = userClaims.FirstOrDefault(uc => uc.Type == ClaimTypes.Name).Value;
                return await _identityService.GetUserOnUsername(username);
            }

            return null;
        }
    }
}
