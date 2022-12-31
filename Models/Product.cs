using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nettbutikk.Models
{
    public class Product : IEntity
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public int Count { get; set; }

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