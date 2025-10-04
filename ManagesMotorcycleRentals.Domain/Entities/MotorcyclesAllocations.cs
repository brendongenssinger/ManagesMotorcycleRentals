using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagesMotorcycleRentals.Domain.Entities
{
    [Table("motorcycles_allocations")]
    public class MotorcyclesAllocations
    {

        [Key]
        [Column("id")]
        public long Id { get; private set; }

        [Column("uid")]
        public Guid Uid { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("expected_return_date")]
        public DateTime ExpectedReturnDate { get; set; }

        [Column("total_cost")]
        public decimal TotalCost { get; set; }
        
        [ForeignKey("MotorcycleId")]
        public Motorcycle Motorcycle { get; set; }
        
        [Column("motorcycle_id")]
        public long? MotorcycleId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [Column("customer_id")]
        public long? CustomerId{ get; set; }

        [ForeignKey("PlanRentalId")]
        public PlanRental PlanRental { get; set; }
        [Column("plan_rental_id")]
        public long? PlanRentalId { get; set; }
    }

    public static class MotorcyclesAllocationsFactory
    {
        public static MotorcyclesAllocations Create(DateTime startDate, DateTime endDate, DateTime expectedReturnDate, decimal totalCost, long motorCycleId, long customerId, long planRentalId)
        {

            return new MotorcyclesAllocations()
            {
                Uid = Guid.NewGuid(),
                StartDate = startDate.ToUniversalTime(),
                EndDate = endDate.ToUniversalTime(), 
                ExpectedReturnDate = expectedReturnDate.ToUniversalTime(),
                CustomerId = customerId, 
                MotorcycleId = motorCycleId,
                PlanRentalId = planRentalId,
                TotalCost = totalCost
            };
        }
    }
}
