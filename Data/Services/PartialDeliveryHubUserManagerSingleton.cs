using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    public class PartialDeliveryHubUserManagerSingleton
    {
        public Dictionary<UserEntity, string> _connectedUsers = new Dictionary<UserEntity, string>();

        public PartialDeliveryHubUserManagerSingleton()
        {

        }
    }
}
