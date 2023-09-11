using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Models.EventModels
{
    [Serializable]
    public class ProductArrivedEvent : IEvent
    {
        public DateTime DateCreated { get; set; }
        public List<Product> Products { get; set; }
    }
}
