using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
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

        public PartialDeliveryHub(UserContextService userContextService)
        {
            _userContextService = userContextService;
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task NotifyUserOfSentPartial(string userId)
        {
            var httpContext = Context.GetHttpContext()
                ?? throw new Exception("Result is null. Connection not associated with HTTP request.");

            var user = _userContextService.GetCurrentUserOnHttpContext(httpContext).Result;

            if (user is not null)
            {
                var clientProxy = Clients.User(userId);
                clientProxy.SendAsync("ReceivePartialDeliveryNotification");
            }
        }
    }
}
