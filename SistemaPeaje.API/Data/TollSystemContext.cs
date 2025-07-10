using Microsoft.EntityFrameworkCore;
using SistemaPeaje.API.Models;

namespace SistemaPeaje.API.Data
{
    public class TollSystemContext : DbContext
    {
        public TollSystemContext(DbContextOptions<TollSystemContext> options) : base(options)
        {
        }
        
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TollStation> TollStations { get; set; }
        public DbSet<TollRate> TollRates { get; set; }
        public DbSet<TollTransaction> TollTransactions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Vehicle configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.LicensePlate).IsUnique();
                entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Brand).HasMaxLength(50);
                entity.Property(e => e.Model).HasMaxLength(50);
                entity.Property(e => e.Color).HasMaxLength(50);
            });
            
            // TollStation configuration
            modelBuilder.Entity<TollStation>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Latitude).HasColumnType("decimal(10,6)");
                entity.Property(e => e.Longitude).HasColumnType("decimal(10,6)");
            });
            
            // TollRate configuration
            modelBuilder.Entity<TollRate>(entity =>
            {
                entity.Property(e => e.Rate).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Description).HasMaxLength(100);
                
                entity.HasOne(d => d.TollStation)
                    .WithMany(p => p.TollRates)
                    .HasForeignKey(d => d.TollStationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // TollTransaction configuration
            modelBuilder.Entity<TollTransaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
                entity.Property(e => e.PaymentReference).HasMaxLength(100);
                entity.Property(e => e.Notes).HasMaxLength(500);
                
                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.TollTransactions)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(d => d.TollStation)
                    .WithMany(p => p.TollTransactions)
                    .HasForeignKey(d => d.TollStationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // Seed data
            SeedData(modelBuilder);
        }
        
        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed TollStations
            modelBuilder.Entity<TollStation>().HasData(
                new TollStation
                {
                    Id = 1,
                    Name = "Peaje Norte",
                    Location = "Autopista Norte Km 15",
                    Description = "Peaje principal de la autopista norte",
                    Latitude = 4.7110m,
                    Longitude = -74.0721m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new TollStation
                {
                    Id = 2,
                    Name = "Peaje Sur",
                    Location = "Autopista Sur Km 25",
                    Description = "Peaje principal de la autopista sur",
                    Latitude = 4.5981m,
                    Longitude = -74.0758m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );
            
            // Seed TollRates
            var tollRates = new List<TollRate>();
            var vehicleTypes = Enum.GetValues<VehicleType>();
            var baseRates = new Dictionary<VehicleType, decimal>
            {
                { VehicleType.Motorcycle, 5000m },
                { VehicleType.Car, 8000m },
                { VehicleType.Van, 12000m },
                { VehicleType.Truck, 20000m },
                { VehicleType.Bus, 25000m },
                { VehicleType.Trailer, 35000m }
            };
            
            int rateId = 1;
            for (int stationId = 1; stationId <= 2; stationId++)
            {
                foreach (var vehicleType in vehicleTypes)
                {
                    tollRates.Add(new TollRate
                    {
                        Id = rateId++,
                        TollStationId = stationId,
                        VehicleType = vehicleType,
                        Rate = baseRates[vehicleType],
                        Description = $"Tarifa para {vehicleType}",
                        EffectiveDate = DateTime.UtcNow,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }
            
            modelBuilder.Entity<TollRate>().HasData(tollRates);
        }
    }
}
