using Nettbutikk.Data.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nettbutikk.Models
{
    public class OrderReceipt
    {
        public string Message { get; set; } = "Thank you for shopping with us. We will take care of your order as fast as possible.";
        public List<ProductDTO> OrderedProducts { get; set; }
        [JsonIgnore]
        public Guid OrderId { get; set; }

        public OrderReceipt(Guid id, List<ProductDTO> productDTOs)
        {
            OrderedProducts = productDTOs;
            OrderId = id;
        }

        public OrderReceipt()
        {

        }
    }
}