using System;

namespace Nettbutikk.CustomExceptions
{
    /// <summary>
    /// Exception thrown when customer tries to order a product that is sold out, or store does not have
    /// enough entities of product to fulfill the order.
    /// </summary>
    public class SoldOutException : Exception
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }

        public SoldOutException(string message) 
            : base(message)
        {
            
        }

        public SoldOutException(string message, string orderId, string productId)
            : base(message)
        {
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
