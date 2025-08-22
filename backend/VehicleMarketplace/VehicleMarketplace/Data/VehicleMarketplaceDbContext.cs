using Microsoft.EntityFrameworkCore;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Data
{
    public class VehicleMarketplaceDbContext : DbContext
    {

        public VehicleMarketplaceDbContext(DbContextOptions<VehicleMarketplaceDbContext> options) : base(options) 
        {
        }

        // configure relationships between models, configure models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /* Map Car table */
            // You have to use this approach, because Car class extends the Vehicle class
            // (Vechilce class should NOT BE mapped in table)
            modelBuilder.Entity<Car>(entity => {

                entity.HasKey(car => car.Vin); // primary key
                entity.Property(car => car.Vin).HasMaxLength(17);

                //foreign key are defined in class
                // ...

                entity.Property(car => car.Vin).HasColumnName("vin");
                entity.Property(car => car.Name).HasColumnName("name");
                entity.Property(car => car.Description).HasColumnName("description");
                entity.Property(car => car.Price).HasColumnName("price");
                entity.Property(car => car.Capacity).HasColumnName("capacity");
                entity.Property(car => car.Power).HasColumnName("power");
            });

            modelBuilder.Entity<Car>().HasMany(car => car.Images)
                .WithOne(image => image.Car)
                .HasForeignKey(image => image.CarVin)
                .OnDelete(DeleteBehavior.Cascade); // if you delete the Car, also delete the car's images

            /* Map Motorcycle table */
            modelBuilder.Entity<Motorcycle>(entity => {

                entity.HasKey(motorcycle => motorcycle.Vin);
                entity.Property(car => car.Vin).HasMaxLength(17);

                entity.Property(motorcycler => motorcycler.Vin).HasColumnName("vin");
                entity.Property(motorcycler => motorcycler.Name).HasColumnName("name");
                entity.Property(motorcycler => motorcycler.Description).HasColumnName("description");
                entity.Property(motorcycler => motorcycler.Price).HasColumnName("price");
                entity.Property(motorcycler => motorcycler.Capacity).HasColumnName("capacity");
                entity.Property(motorcycler => motorcycler.Power).HasColumnName("power");
            });

            modelBuilder.Entity<Motorcycle>().HasMany(motorcycle => motorcycle.Images)
                .WithOne(image => image.Motorcycle)
                .HasForeignKey(image => image.MotorcycleVin)
                .OnDelete(DeleteBehavior.Cascade); // if you delete the Motorcycle, also delete the motorcycle's images

            /* Add starting Makes values */
            modelBuilder.Entity<Make>().HasData(
                new Make { Id = 1, MakeName = "BMW" },
                new Make { Id = 2, MakeName = "Audi" },
                new Make { Id = 3, MakeName = "Dodge" },
                new Make { Id = 4, MakeName = "Alfa Romeo" },
                new Make { Id = 5, MakeName = "Nissan" },
                new Make { Id = 6, MakeName = "Dacia" },
                new Make { Id = 7, MakeName = "Bentley" },
                new Make { Id = 8, MakeName = "Chevrolet" },
                new Make { Id = 9, MakeName = "Ford" },
                new Make { Id = 10, MakeName = "Honda" }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        
        public DbSet<Car> Cars { get; set; }
        
        public DbSet<Motorcycle> Motorcyclers { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Image> Images { get; set; }
    }
}
