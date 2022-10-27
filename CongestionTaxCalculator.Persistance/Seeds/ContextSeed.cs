using CongestionTaxCalculator.Domain.Constants;
using CongestionTaxCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CongestionTaxCalculator.Persistance.Seeds;

public static class ContextSeed
{
    public static IServiceCollection CheckAndSeedData(this IServiceCollection services, bool isStartedWithMain)
    {
        if (!isStartedWithMain)
        {
            return services;
        }

        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            // Migrating application db context and seeding configuration data
            var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            // Since we are using in memory database, that's why we don't need to add migrations
            //applicationDbContext?.Database.Migrate();

            SeedData(applicationDbContext);
        }
        return services;
    }

    private static void SeedData(ApplicationDbContext context)
    {
        // TODO - Add seeding logic here
        #region Seed City
        var defaultCity = new CityEntity()
        {
            Name = "Gothenburg",
            MaxTaxPerDay = 60

        };
        context.Cities.Add(defaultCity);

        #endregion
        #region Seed TaxSchedule

        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("06:00").TimeOfDay,
            EndTime = DateTime.Parse("06:29").TimeOfDay,
            Amount = 8
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("06:30").TimeOfDay,
            EndTime = DateTime.Parse("06:59").TimeOfDay,
            Amount = 13
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("07:00").TimeOfDay,
            EndTime = DateTime.Parse("07:59").TimeOfDay,
            Amount = 18
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("08:00").TimeOfDay,
            EndTime = DateTime.Parse("08:29").TimeOfDay,
            Amount = 13
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("08:30").TimeOfDay,
            EndTime = DateTime.Parse("14:59").TimeOfDay,
            Amount = 8
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("15:00").TimeOfDay,
            EndTime = DateTime.Parse("15:29").TimeOfDay,
            Amount = 13
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("15:30").TimeOfDay,
            EndTime = DateTime.Parse("16:59").TimeOfDay,
            Amount = 18
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("17:00").TimeOfDay,
            EndTime = DateTime.Parse("17:59").TimeOfDay,
            Amount = 13
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("18:00").TimeOfDay,
            EndTime = DateTime.Parse("18:29").TimeOfDay,
            Amount = 8
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("18:30").TimeOfDay,
            EndTime = DateTime.Parse("23:59").TimeOfDay,
            Amount = 0
        });
        context.TaxSchedules.Add(new TaxScheduleEntity()
        {
            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("00:00").TimeOfDay,
            EndTime = DateTime.Parse("05:59").TimeOfDay,
            Amount = 0
        });
        #endregion
        #region TaxFreeDays

        context.TaxFreeDays.Add(new TaxFreeDayEntity()
        {

            CityId = defaultCity.Id,
            StartTime = DateTime.Parse("2022-07-01"),
            EndTime = DateTime.Parse("2022-07-31"),
            IsPublicHoliday = false
        });
        #endregion
        #region TaxExemptVehicles

        context.TaxExemptVehicles.Add(new TaxExemptVehicleEntity()
        {

            CityId = defaultCity.Id,
            Type = VehicleType.Bus

        });
        context.TaxExemptVehicles.Add(new TaxExemptVehicleEntity()
        {

            CityId = defaultCity.Id,
            Type = VehicleType.Emergency

        });
        context.TaxExemptVehicles.Add(new TaxExemptVehicleEntity()
        {

            CityId = defaultCity.Id,
            Type = VehicleType.Diplomat

        });
        context.TaxExemptVehicles.Add(new TaxExemptVehicleEntity()
        {

            CityId = defaultCity.Id,
            Type = VehicleType.Motorcycle

        });
        context.TaxExemptVehicles.Add(new TaxExemptVehicleEntity()
        {

            CityId = defaultCity.Id,
            Type = VehicleType.Military

        });
        context.TaxExemptVehicles.Add(new TaxExemptVehicleEntity()
        {

            CityId = defaultCity.Id,
            Type = VehicleType.Foreign

        });
        #endregion
        context.SaveChanges();
    }
}