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
        public Event CreateDeliveryEvent(string eventName, List<Product> products, DateTime? expectedDate = null)
        {
            IEvent evt;

            if (eventName.Equals(EventTypes.ProductArrivedEvent))
            {
                evt = new ProductArrivedEvent()
                {
                    DateCreated = DateTime.UtcNow,
                    Products = products
                };
            }    

            else if (eventName.Equals(EventTypes.DeliveryLateEvent))
            {
                if (expectedDate is null)
                    throw new Exception($"DeliveryLateEvent must have an expected date. {expectedDate} can not be null.");

                evt = new DeliveryLateEvent()
                {
                    Id = Guid.NewGuid(),
                    Products = products
                };
            }

            else throw new Exception("Invalid event type");

            string data = JsonSerializer.Serialize(evt);

            return new Event()
            {
                EventName = eventName,
                DateCreated = DateTime.UtcNow,
                IsHandled = false,
                JsonData = data
            };
        }
    }
}
