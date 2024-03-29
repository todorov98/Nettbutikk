﻿using Microsoft.AspNetCore.Http;
using Nettbutikk.Models;
using System;
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
            if (httpContext is null)
                throw new ArgumentNullException("Argument httpContext can not be null");

            var identity = httpContext.User.Identity as ClaimsIdentity;

            if (identity is not null)
            {
                var userClaims = identity.Claims;

                var username = userClaims.FirstOrDefault(uc => uc.Type == ClaimTypes.Name).Value;

                if (username is null)
                    throw new Exception("Could not find username on given httpContext");

                return await _identityService.GetUserOnUsername(username);
            }

            return null;
        }
    }
}
