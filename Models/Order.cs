using Microsoft.Extensions.Logging;
using Nettbutikk.Data;
using Nettbutikk.State;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    /// <summary>
    /// Represents an order placed by the customer. 
    /// </summary>
    public class Order : IEntity
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public DateTime? DateFulfilled { get; set; }
        public double Price { get; set; }
        public string Stage { get; set; }
        public bool WantsPartialDelivery { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public UserEntity User { get; set; }
        [JsonIgnore]
        public ICollection<ProductOrderRelation> ProductOrderRelations { get; set; }

        [NotMapped]
        private readonly WebStoreContext _webStoreContext;
        [NotMapped]
        private readonly ILogger<Order> _errorLogger;

        /// <summary>
        /// Constructor that initializes the private DbContext field in the class.
        /// </summary>
        /// <param name="webStoreContext"></param>
        public Order(WebStoreContext webStoreContext, ILogger<Order> errorLogger)
        {
            _webStoreContext = webStoreContext;
            _errorLogger = errorLogger;
        }

        public Order(WebStoreContext webStoreContext)
        {
            _webStoreContext = webStoreContext;
        }

        public Order()
        {
            
        }

        /// <summary>
        /// Advances the stage of an order as it is being processed by the store.
        /// </summary>
        /// <returns>Returns true if stage was updated, and false if stage was not updated.</returns>
        public AdvanceOrderStageConfirmation AdvanceStage(bool advancedByAdmin)
        {
            var previousStage = Stage;

            if (Stage.Equals(OrderStages.Received))
            {
                Stage = OrderStages.InProcess;
            }

            else if (Stage.Equals(OrderStages.InProcess))
            {
                Stage = OrderStages.Sent;
            }

            else if (Stage.Equals(OrderStages.Sent))
            {
                Stage = OrderStages.Delivered;
                DateFulfilled = DateTime.Now;
            }

            else if (Stage.Equals(OrderStages.Cancelled))
                throw new Exception("Cancelled order can't be advanced.");

            else throw new Exception("Unexpected error. Couldn't advance stage, and none of the cases were true.");

            var newStage = Stage;

            return new AdvanceOrderStageConfirmation
            {
                OrderId = Id,
                PreviousStage = previousStage,
                NewStage = newStage,
                AdvancedByAdmin = advancedByAdmin
            };
        }

        /// <summary>
        /// Cancels the specific order.
        /// </summary>
        /// <returns>Returns an object of type CancelOrderConfirmation on succesful cancellation, or null if
        /// cancellation was not succesful.</returns>
        public CancelOrderConfirmation CancelOrder()
        {
            try
            {
                if (Stage != OrderStages.Cancelled)
                {
                    Stage = OrderStages.Cancelled;

                    var cancelOrderConfirmation = new CancelOrderConfirmation
                    {
                        CancelDate = DateTime.Now.AddMilliseconds(-15.0),
                        Order = this,
                        OrderId = Id
                    };

                    return cancelOrderConfirmation;
                }

                else return null;
            }

            catch (Exception e)
            {
                _errorLogger.LogError($"Error location: {e.StackTrace}, Exception type: {e.GetType()}, " +
                    $"Error message: {e.Message}");

                throw new Exception("Cancellation failed. Already cancelled.");
            }
        }

        /// <summary>
        /// Calculates and sets the total price for the order.
        /// </summary>
        public void CalculatePrize()
        {
            double totalPrice = 0;

            if (ProductOrderRelations.Count == 0 || ProductOrderRelations is null)
                throw new Exception("ERROR: Trying to calculate total order price without established product-order-relations.");

            foreach (var productOrderRelation in ProductOrderRelations)
            {
                if (productOrderRelation.OrderId == Id)
                    totalPrice += productOrderRelation.Product.Price * productOrderRelation.ProductCount;
            }

            Price = totalPrice;
        }

        /// <summary>
        /// Persists (stores) the specific object to the database. This method is used when a new record is created, not for 
        /// updating existing records.
        /// </summary>
        /// <returns>Returns true if the object was persisted succesfully, or false if not.</returns>
        public bool Commit()
        {
            try
            {
                if (Exists())
                {
                    _webStoreContext.SaveChanges();
                    return true;
                }

                else
                {
                    _webStoreContext.Orders.Add(this);
                    _webStoreContext.SaveChanges();
                    return true;
                }
            }
            
            catch (Exception e)
            {
                _errorLogger.LogError($"Error location: {e.StackTrace}, Exception type: {e.GetType()}, " +
                    $"Error message: {e.Message}");

                throw new Exception("ERROR: Order commit failed.");
            }
        }

        /// <summary>
        /// Checks if the current Order object exists in the database, or is already being tracked by the DbContext.
        /// </summary>
        /// <returns>Returns true if object exists, or false if not.</returns>
        public bool Exists()
        {
            try
            {
                if (_webStoreContext.Orders.Find(Id) != null)
                    return true;

                else return false;
            }
            
            catch (Exception e)
            {
                _errorLogger.LogError($"Error location: {e.StackTrace}, Exception type: {e.GetType()}, " +
                    $"Error message: {e.Message}");

                throw new Exception("ERROR: Order existence check failed.");
            }
        }
    }
}