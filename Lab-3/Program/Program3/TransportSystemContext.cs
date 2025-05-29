using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Program3.Models;

namespace Program3
{
    public class TransportSystemContext : DbContext
    {
        public TransportSystemContext(DbContextOptions<TransportSystemContext> options)
            : base(options) { }

        public DbSet<BusModel> Buses { get; set; }
        public DbSet<DriverModel> Drivers { get; set; }
        public DbSet<RouteModel> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфігурація зв'язків за допомогою Fluent API

            // Один-до-одного: BusModel -> DriverModel
            modelBuilder.Entity<BusModel>()
                .HasOne(b => b.Driver)
                .WithOne(d => d.Bus)
                .HasForeignKey<DriverModel>(d => d.BusModelId);

            // Один-до-багатьох: RouteModel -> BusModel
            modelBuilder.Entity<RouteModel>()
                .HasMany(r => r.Buses)
                .WithOne(b => b.Route)
                .HasForeignKey(b => b.RouteModelId);
        }
    }
}