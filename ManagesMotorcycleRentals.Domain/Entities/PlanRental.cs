using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagesMotorcycleRentals.Domain.Entities
{
    [Table("plan_rental")]
    public class PlanRental
    {
        [Key]
        [Column("id")]
        public long Id { get; private set; }
        [Column("uid")]
        public Guid Uid { get; set; }
        [Column("days")]
        public int Days { get; set; }
        [Column("price_per_day")]
        public decimal PricePerDay { get; set; }
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        public ICollection<MotorcyclesAllocations> Rentals { get; set; }

        public PlanRental()
        {
            
        }
        public PlanRental(int id)
        {
            Id = id;    
        }
    }
}
