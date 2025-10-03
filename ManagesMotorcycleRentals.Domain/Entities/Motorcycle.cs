using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagesMotorcycleRentals.Domain.Entities
{
    [Table("motorcycle")]
    public class Motorcycle
    {
        public Motorcycle()
        {
                
        }

        [Key]
        [Column("id")]
        public long Id { get; private set; }
        [Column("uid")]
        public Guid Uid { get; set; }
        [Column("year")]
        [Required]
        public int Year { get; set; }
        [Column("model")]
        [MaxLength(100)]
        public string Model { get; set; }
        [Column("license_plate")]
        public string LicensePlate { get; set; }

    }

    public static class MotorcyleFactory
    {
        public static Motorcycle Create(int ano, string modelo, string placa)
        {
            return new Motorcycle()
            {
                Uid = Guid.NewGuid(),
                Year = ano,
                Model = modelo,
                LicensePlate = placa
            };
        }

        public static Motorcycle Update(Motorcycle motorcycle, string licensePlate)
        {
            motorcycle.LicensePlate = licensePlate;
            return motorcycle;
        }
    }
}
