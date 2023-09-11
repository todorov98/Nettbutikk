using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Factories
{
    public class PartialDeliveryFactory
    {
        public PartialDeliveryFactory()
        {

        }

        public PartialDelivery CreatePartialDelivery()
        {
            return new PartialDelivery();
        }
    }
}
