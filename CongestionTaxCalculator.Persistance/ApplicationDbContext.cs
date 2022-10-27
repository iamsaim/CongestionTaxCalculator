using CongestionTaxCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Persistance
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<TaxScheduleEntity> TaxSchedules { get; set; }
        public DbSet<TaxFreeDayEntity> TaxFreeDays { get; set; }
        public DbSet<CongestionTaxEntity> CongestionTaxes { get; set; }
        public DbSet<TaxExemptVehicleEntity> TaxExemptVehicles { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}