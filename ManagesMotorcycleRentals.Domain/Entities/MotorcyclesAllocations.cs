using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Domain.Entities
{
    public class MotorcyclesAllocations
    {
        public Guid Id { get; set; }
        public Guid MotorcycleId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        // Navigation properties
        public Motorcycle Motorcycle { get; set; }
        public Customer Customer { get; set; }
    }
}
