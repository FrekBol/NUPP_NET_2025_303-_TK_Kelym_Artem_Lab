namespace AviationSystem.REST.Data;
using AviationSystem.REST.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Базова таблиця з усіма спільними полями
    public DbSet<AircraftModel> Aircrafts { get; set; }
    // Таблиці-нащадки
    public DbSet<MilitaryAircraftModel> MilitaryAircrafts { get; set; }
    public DbSet<CivilAircraftModel> CivilAircrafts { get; set; }
    public DbSet<CargoAircraftModel> CargoAircrafts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Базова сутність
        modelBuilder.Entity<AircraftModel>(entity =>
        {
            entity.ToTable("Aircrafts");
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Manufacturer)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.ModelName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.ProductionYear)
                  .IsRequired();

            entity.Property(a => a.MaxSpeedKmh)
                  .IsRequired();

            entity.Property(a => a.RangeKm)
                  .IsRequired();

            entity.Property(a => a.ServiceCeilingMeters)
                  .IsRequired();

            entity.Property(a => a.EmptyWeightKg)
                  .IsRequired();
        });

        // Наслідування Table-Per-Type: кожен клас у свою таблицю
        modelBuilder.Entity<MilitaryAircraftModel>()
                    .ToTable("MilitaryAircrafts");
        modelBuilder.Entity<CivilAircraftModel>()
                    .ToTable("CivilAircrafts");
        modelBuilder.Entity<CargoAircraftModel>()
                    .ToTable("CargoAircrafts");
    }
}