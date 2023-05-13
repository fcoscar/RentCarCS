using Microsoft.EntityFrameworkCore;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Configurations;

namespace RentCar.Infraestructure.Context
{
    public class RentCarContext : DbContext
    {
        public RentCarContext()
        {
            
        }

        public RentCarContext(DbContextOptions<RentCarContext> options) : base(options)
        {
            
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categorias { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfigurationRentCar();
        }
    }
}