using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Application.DTOs
{
    public class PlansRentalDto
    {
        public Guid Uid { get; set; }
        public int Days { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
