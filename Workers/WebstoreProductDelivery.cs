using Microsoft.Extensions.Hosting;
using Nettbutikk.Data;
using Nettbutikk.Data.Events;
using Nettbutikk.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nettbutikk.Workers
{
    /// <summary>
    /// Worker that informs the rest of the system when a product has been delivered to the store.
    /// When products are delivered, this class creates an event that signals to the system that
    /// the product is delivered.
    /// </summary>
    public class WebstoreProductDelivery : BackgroundService
    {
        private WebStoreContext _context;
        private ProductFactory _productFactory;
        private EventFactory _eventFactory;

        public WebstoreProductDelivery(WebStoreContext webStoreContext, ProductFactory productFactory, EventFactory eventFactory)
        {
            _context = webStoreContext;
            _productFactory = productFactory;
            _eventFactory = eventFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(20000);

                try
                {
                    var arrivedProducts = _productFactory.CreateProducts();
                    await _context.Products.AddRangeAsync(arrivedProducts);

                    var evt = _eventFactory.CreateProductsArrivedEvent("ProductArrivedEvent", arrivedProducts);
                    await _context.Events.AddAsync(evt);

                    await _context.SaveChangesAsync();
                }

                catch(Exception ex)
                {
                    throw new Exception(ex.Message + ". Something failed when receiving products to the store and storing them to database.");
                }
                
            }

            throw new NotImplementedException();
        }
    }
}
