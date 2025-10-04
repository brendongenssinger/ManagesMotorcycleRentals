using ManagesMotorcycleRentals.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace ManagesMotorcycleRentals.Infrastructure.Context
{
    public class ManagesMotorcyclesRentalsDbContext : DbContext
    {
        
        public ManagesMotorcyclesRentalsDbContext(DbContextOptions<ManagesMotorcyclesRentalsDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();             
        }

        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<PlanRental> PlanRentals { get; set; }
        public DbSet<MotorcyclesAllocations> MotorcyclesAllocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Customer>()
                .HasOne(c=> c.MotorcyclesAllocations)
                .WithOne(r=> r.Customer)
                .HasForeignKey<Customer>(c=> c.MotorCycleAllocationsId);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Cnpj)
                .IsUnique();

            modelBuilder.Entity<MotorcyclesAllocations>()
                .HasOne(r => r.Customer)
                .WithOne(c => c.MotorcyclesAllocations)
                .HasForeignKey<MotorcyclesAllocations>(r => r.MotorcycleId);

            modelBuilder.Entity<MotorcyclesAllocations>()
                .HasOne(r => r.PlanRental)
                .WithMany(p => p.Rentals)
                .HasForeignKey(r => r.PlanRentalId);

            modelBuilder.Entity<Motorcycle>()
                .HasOne(m=> m.Rentals)
                .WithOne(r=> r.Motorcycle)
                .HasForeignKey<MotorcyclesAllocations>(r=> r.MotorcycleId);

            modelBuilder.Entity<PlanRental>()
                .HasKey(x => x.Id);


            modelBuilder.Entity<PlanRental>().HasData(
                 new PlanRental(1) { Uid = Guid.Parse("84c0ab8e-38d3-4221-ab0a-e61cee9a0638"), Days = 7, PricePerDay = 30.00m, TotalPrice = 210.00m },
                 new PlanRental(2) { Uid = Guid.Parse("67c2ecf9-3e8c-4ea8-9ed7-979eb7a105ff"), Days = 15, PricePerDay = 28.00m, TotalPrice = 420.00m },
                 new PlanRental(3) { Uid = Guid.Parse("a0ddc745-be2f-4139-9718-7d642c2502d9"), Days = 30, PricePerDay = 22.00m, TotalPrice = 660.00m },
                 new PlanRental(4) { Uid = Guid.Parse("83eade55-13e4-40ce-8f8c-9ccafdac38cb"), Days = 45, PricePerDay = 20.00m, TotalPrice = 900.00m },
                 new PlanRental(5) { Uid = Guid.Parse("99e5838a-9aa4-4445-a514-be4825466a6a"), Days = 50, PricePerDay = 18.00m, TotalPrice = 900.00m }
             );
        }

    }
}
