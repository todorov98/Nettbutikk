using System;

namespace Nettbutikk.Data.DTO
{
    [Serializable]
    public class ProductDTO : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; } //number of items of specific product
    }
}
