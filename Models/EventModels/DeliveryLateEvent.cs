using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nettbutikk.Data.Events;
using Nettbutikk.Models.EventModels;

namespace Nettbutikk.Models.EventModels
{
    [Serializable]
    public class DeliveryLateEvent : IEvent
    {
        public DateTime Expected { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Product> Products { get; set; }
    }
}
