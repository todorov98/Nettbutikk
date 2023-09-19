using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nettbutikk.Data;
using Nettbutikk.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nettbutikk.Workers
{
    /// <summary>
    /// Listens for events in the database.
    /// </summary>
    public class EventListener : BackgroundService
    {
        private readonly WebStoreContext _context;
        private readonly PartialDeliveryService _partialDeliveryService;

        public EventListener(PartialDeliveryService partialDeliveryService, IServiceScopeFactory serviceScopeFactory)
        {
            _context = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<WebStoreContext>();
            _partialDeliveryService = partialDeliveryService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var evts = _context.Events.Where(evt => !evt.IsHandled).ToList(); // gets unhandled events
                Task.Run(async() => await _partialDeliveryService.HandleProductsReceivedForPartialDeliveryEvents(evts));
                // here we should send the events to some signalR service that can transmit the messages onward to the appropriate receivers
            }

            throw new NotImplementedException();
        }
    }
}
