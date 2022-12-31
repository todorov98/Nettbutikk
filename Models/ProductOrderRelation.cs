using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    /// <summary>
    /// Represents a relation between a single product and an order. An order can have multiple products in it, and a product
    /// can be present in multiple orders.
    /// </summary>
    public class ProductOrderRelation
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductCount { get; set; }

        /// <summary>
        /// Creates an instance of ProductOrderRelation and initializes all properties.
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="order"></param>
        /// <param name="verifier"></param>
        public ProductOrderRelation(Product product, Order order, int productCount)
        {
            Id = Guid.NewGuid();
            Order = order;
            OrderId = order.Id;
            Product = product;
            ProductId = product.Id;
            ProductCount = productCount;
        }

        public ProductOrderRelation()
        {

        }
    }
}
