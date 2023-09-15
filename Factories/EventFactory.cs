using Nettbutikk.Data.Events;
using Nettbutikk.Models;
using Nettbutikk.Models.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nettbutikk.Factories
{
    public class EventFactory
    {
        public Event CreateDeliveryEvent(string eventName, List<Product> products, Guid? partialId = null)
        {
            IEvent evt;

            if (partialId is null)
                evt = new ProductArrivedEvent()
                {
                    DateCreated = DateTime.UtcNow,
                    Products = products
                };

            else
            {
                evt = new DeliveryLateEvent()
                {
                    Id = Guid.NewGuid(),
                    PartialDeliveryId = (Guid)partialId
                };
            }

            string data = JsonSerializer.Serialize(evt);

            return new Event()
            {
                EventName = eventName,
                DateTime = DateTime.UtcNow,
                IsHandled = false,
                JsonData = data
            };
        }
    }
}
