using Microsoft.Extensions.Hosting;
using Nettbutikk.Data;
using Nettbutikk.Data.Events;
using Nettbutikk.Factories;
using Nettbutikk.Models.EventModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Nettbutikk.Models;
using Microsoft.EntityFrameworkCore;


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
            var rnd = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);

                try
                {
                    Event lateEvt;
                    Event arrivedEvt;
                    var products = _context.Products.AsQueryable().AsNoTracking().ToList();

                    // creates possibility of product being late, which triggers events, if true product is late
                    if (rnd.Next(1, 20) > 13)
                    {
                        var expectedDate = DateTime.UtcNow.AddMinutes(3.0);
                        lateEvt = _eventFactory.CreateDeliveryEvent(EventTypes.DeliveryLateEvent, products);
                        await _context.Events.AddAsync(lateEvt);
                        await _context.SaveChangesAsync();

                        await Task.Delay(3000);
                    }

                    await _context.Products.ForEachAsync((product) => { product.Count++; });

                    foreach (var product in _context.Products)
                        product.Count++;

                    arrivedEvt = _eventFactory.CreateDeliveryEvent(EventTypes.ProductArrivedEvent, products);
                    await _context.Events.AddAsync(arrivedEvt);

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
