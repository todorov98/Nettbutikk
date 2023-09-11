using Nettbutikk.Data.Events;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nettbutikk.Factories
{
    public class EventFactory
    {
        public Event CreateProductsArrivedEvent(string eventName, List<Product> products)
        {
            string data = JsonSerializer.Serialize(products);

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
