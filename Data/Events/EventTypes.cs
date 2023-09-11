using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Events
{
    public static class EventTypes
    {
        public static readonly string ProductArrivedEvent = "ProductArrivedEvent";
        public static readonly string DeliveryLateEvent = "DeliveryLateEvent";
    }
}
