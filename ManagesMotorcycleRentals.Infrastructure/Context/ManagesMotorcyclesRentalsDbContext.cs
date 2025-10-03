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
            Database.Migrate();
            Database.EnsureCreated();
            var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
            if(databaseCreator.Exists() == false)
                databaseCreator.CreateTables();            
        }

        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
       
    }
}
