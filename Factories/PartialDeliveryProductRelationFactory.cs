using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Factories
{
    public class PartialDeliveryProductRelationFactory
    {
        public PartialDeliveryProductRelationFactory()
        {

        }

        public PartialDeliveryProductRelation CreatePartialDeliveryProductRelation(PartialDelivery partialDelivery, Product product, int count)
        {
            return new PartialDeliveryProductRelation(partialDelivery, product, count);
        }
    }
}
