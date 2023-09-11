using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nettbutikk.Models
{
    public class PartialDelivery : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public UserEntity User { get; set; }

        public bool IsFulfilled { get; set; }

        public PartialDelivery()
        {
            Id = Guid.NewGuid();
            IsFulfilled = false;
        }

        [JsonIgnore]
        public ICollection<PartialDeliveryProductRelation> PartialDeliveryProductRelations { get; set; }

        public bool Commit()
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }
    }
}
