using System;
using System.Collections.Generic;

namespace Nettbutikk.Data.DTO
{
    [Serializable]
    public class OrderDTO : IDto
    {
        public Guid Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public DateTime DateFulfilled { get; set; }
        public double Price { get; set; }
        public string Stage { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
