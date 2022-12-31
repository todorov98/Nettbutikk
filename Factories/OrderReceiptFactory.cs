using Nettbutikk.Data.DTO;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;

namespace Nettbutikk.Factories
{
    public class OrderReceiptFactory
    {
        public OrderReceipt CreateOrderReceipt(Guid id, List<ProductDTO> productDTOs)
        {
            var receipt = new OrderReceipt(id, productDTOs);
            return receipt;
        }
    }
}
