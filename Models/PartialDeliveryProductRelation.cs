using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Models
{
    /// <summary>
    /// Represents a relation between a product and a partial delivery.
    /// </summary>
    public class PartialDeliveryProductRelation
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Prodcut")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("PartialDelivery")]
        public Guid PartialDeliveryId { get; set; }
        public PartialDelivery PartialDelivery { get; set; }
        public int ProductCount { get; set; }

        public PartialDeliveryProductRelation(PartialDelivery partialDelivery, Product product, int productCount)
        {
            Id = Guid.NewGuid();
            PartialDelivery = partialDelivery;
            PartialDeliveryId = partialDelivery.Id;
            Product = product;
            ProductId = product.Id;
            ProductCount = productCount;
        }
    }
}
