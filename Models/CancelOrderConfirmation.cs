using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettbutikk.Models
{
    public class CancelOrderConfirmation
    {
        [JsonIgnore]
        [Key]
        public Guid Id { get; set; }
        public DateTime CancelDate { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public bool CancelledByAdmin { get; set; }

        public CancelOrderConfirmation()
        {
            
        }
    }
}