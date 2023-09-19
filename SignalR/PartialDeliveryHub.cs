using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Nettbutikk.Data.DTO;
using Nettbutikk.Data.Services;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nettbutikk.SignalR
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartialDeliveryHub : Hub
    {
        private readonly UserContextService _userContextService;
        private PartialDeliveryHubUserManagerSingleton _connectedUserManager;

        public PartialDeliveryHub(UserContextService userContextService, PartialDeliveryHubUserManagerSingleton connectedUserManager)
        {
            _userContextService = userContextService;
            _connectedUserManager = connectedUserManager;
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            var user = GetCurrentConnectingUser();

            if (!_connectedUserManager._connectedUsers.ContainsKey(user))
                _connectedUserManager._connectedUsers.Add(user, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        [Authorize]
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = GetCurrentConnectingUser();
            _connectedUserManager._connectedUsers.Remove(user);

            return base.OnDisconnectedAsync(exception);
        }

        private UserEntity GetCurrentConnectingUser()
        {
            var httpContext = Context.GetHttpContext()
               ?? throw new Exception("Result is null. Connection not associated with HTTP request.");

            return _userContextService.GetCurrentUserOnHttpContext(httpContext).Result;
        }
    }
}
