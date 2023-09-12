using Microsoft.AspNetCore.Identity;
using Nettbutikk.Factories;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    public class PartialDeliveryService
    {
        private readonly WebStoreContext _webStoreContext;
        private readonly PartialDeliveryFactory _partialDeliveryFactory;
        private readonly UserManager<UserEntity> _userManager;

        public PartialDeliveryService(WebStoreContext webStoreContext, PartialDeliveryFactory partialDeliveryFactory, UserManager<UserEntity> userManager)
        {
            _webStoreContext = webStoreContext;
            _partialDeliveryFactory = partialDeliveryFactory;
            _userManager = userManager;
        }

        public Task<List<PartialDelivery>> CheckIfUserHasPartialDeliveries(string userId)
        {
            if (userId is not null)
            {
                if (_webStoreContext.PartialDeliveries.Any(pd => pd.UserId.Equals(userId)))
                {
                    var partials = _webStoreContext.PartialDeliveries.Where(pd => pd.UserId.Equals(userId)).ToList();
                    return Task.FromResult(partials);
                }

                return null;
            }

            throw new ArgumentNullException("userId can not be null when checking for partial deliveries");
        }
    }
}
