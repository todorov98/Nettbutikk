using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Models.EventModels
{
    /// <summary>
    /// Represents an event in the webstore system, (e.g., ProductArrivedEvent).
    /// </summary>
    public class Event : IEvent
    {
        public string EventName { get; set; }
        public string JsonData { get; set; }
        public bool IsHandled { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? WasHandledAt { get; set; }
    }
}
