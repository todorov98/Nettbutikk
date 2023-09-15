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
        public Guid Id { get; set; }
        public Guid PartialDeliveryId { get; set; }
    }
}
