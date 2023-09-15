using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Factories
{
    public static class PartialDeliveryProductRelationFactory
    {
        public static PartialDeliveryProductRelation CreatePartialDeliveryProductRelation(PartialDelivery partialDelivery, Product product, int count)
        {
            return new PartialDeliveryProductRelation(partialDelivery, product, count);
        }
    }
}
