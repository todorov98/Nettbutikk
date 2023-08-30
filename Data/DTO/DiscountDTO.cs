using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Data.DTO
{
    public class DiscountDTO : IDto
    {
        public Guid ProductId { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
