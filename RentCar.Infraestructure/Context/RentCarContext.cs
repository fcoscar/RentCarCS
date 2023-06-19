using Microsoft.EntityFrameworkCore;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Configurations;
using RentCar.Infraestructure.Models;

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

        public DbSet<Car> Car { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Alquiler> Alquiler { get; set; }
        public DbSet<User> User { get; set; }
        //public DbSet<UserCarModel> UserCarModels { get; set; }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.AddConfigurationRentCar();
        //     base.OnModelCreating(modelBuilder);
        // }
    }
}