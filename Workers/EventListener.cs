using Microsoft.Extensions.Hosting;
using Nettbutikk.Data;
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

        public EventListener(WebStoreContext context)
        {
            _context = context;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var evts = _context.Events.Where(evt => !evt.IsHandled); // gets unhandled events
                evts.ToList();
                // here we should send the events to some signalR service that can transmit the messages onward to the appropriate receivers
            }

            throw new NotImplementedException();
        }
    }
}
