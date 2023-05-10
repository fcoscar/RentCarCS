using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentCar.domain.Entity;

namespace RentCar.Infraestructure.Configurations
{
    public static class RentCarConfiguration
    {
        public static void AddConfigurationRentCar(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Alquiler>(entity =>
            {
                entity.Property(e => e.From)
                    .HasColumnType("datetime");
                entity.Property(e => e.To)
                    .HasColumnType("datetime");
                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.HasOne<Car>(e => e.Car)
                    .WithMany(g => g.Alquilers)
                    .HasForeignKey(s => s.Id);
                entity.Property(e =>e.TotalPrice)
                    .HasColumnType("decimal(5,3)");
            });
                
            
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne<Category>(e => e.Category)
                    .WithMany(g => g.Cars)
                    .HasForeignKey(s => s.Id)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.Property(e => e.Descipcion)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.PricePerDay)
                    .HasColumnType("decimal(5,3)");
                entity.Property(e => e.FechaElimino)
                    .HasColumnType("datetime");
                entity.Property(e => e.From)
                    .HasColumnType("datetime");
                entity.Property(e => e.To)
                    .HasColumnType("datetime");
                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
        }
        
    }
}