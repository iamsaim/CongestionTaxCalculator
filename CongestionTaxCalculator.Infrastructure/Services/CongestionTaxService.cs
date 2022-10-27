using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using CongestionTaxCalculator.Application.Features.City.Query;
using CongestionTaxCalculator.Application.Features.CongestionTax.Command;
using CongestionTaxCalculator.Application.Features.CongestionTax.Query;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Query;
using CongestionTaxCalculator.Application.Features.TaxFreeDay.Query;
using CongestionTaxCalculator.Application.Features.TaxSchedule.Query;
using CongestionTaxCalculator.Application.Features.Vehicle.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Infrastructure.Services
{
    public class CongestionTaxService : ICongestionTaxService
    {
        private readonly ILogger<CongestionTaxService> _logger;
        private readonly IMediator _mediator;

        public CongestionTaxService(ILogger<CongestionTaxService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<List<GetAllCongestionTaxResponseDTO>> GetAllCongestionTax(GetAllCongestionTaxRequestDTO data)
        {
            var response = await _mediator.Send(new GetAllCongestionTaxQuery
            {
                data = data
            });

            return response;
        }

        public async Task<GenericResponse<bool>> InsertCongestionTax(InsertCongestionTaxDTO request)
        {
            // Calculating tax
            var totalTax = await CalculateTax(request.VehicleId, request.CityId, request.TaxOn);

            var response = await _mediator.Send(new InsertCongestionTaxCommand()
            {
                data = request,
                TaxAmount = totalTax
            });

            var message = "CongestionTax added successfully";

            if (!response)
            {
                message = "Failed to insert CongestionTax";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> UpdateCongestionTax(UpdateCongestionTaxDTO request)
        {
            // Calculating tax
            var totalTax = await CalculateTax(request.VehicleId, request.CityId, request.TaxOn);

            var response = await _mediator.Send(new UpdateCongestionTaxCommand()
            {
                data = request
            });

            var message = "CongestionTax updated successfully";

            if (!response)
            {
                message = "Failed to update CongestionTax";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> ToggleDeleteCongestionTax(ToggleDeleteDTO request)
        {
            var response = await _mediator.Send(new ToggleDeleteCongestionTaxCommand()
            {
                data = request
            });

            var message = "CongestionTax deleted successfully";

            if (!response)
            {
                message = "Failed to deleted CongestionTax";
            }

            return new GenericResponse<bool>(response, message);
        }


        /* Private Methods */

        private async Task<double> CalculateTax(Guid vehicleId, Guid cityId, DateTime taxOn)
        {
            double taxAmount = 0;

            // Checking if vehicle is tax exempt or not
            if (!await IsVehicleTaxExempt(vehicleId, cityId) && !await IsTaxFreeDay(cityId, taxOn))
            {
                // Calculating tax
                taxAmount = await CalculateTaxOfVehicle(vehicleId, cityId, taxOn);
            }

            return taxAmount;
        }

        private async Task<bool> IsVehicleTaxExempt(Guid vehicleId, Guid cityId)
        {
            var vehicles = await _mediator.Send(new GetAllVehiclesQuery());

            var vehicle = vehicles.FirstOrDefault(x => x.Id == vehicleId);

            if (vehicle != null)
            {
                // Getting tax exempt vehicle of cities
                var taxExemptVehicles = await _mediator.Send(new GetTaxExemptVehicleByCityIdQuery()
                {
                    CityId = cityId
                });
                return taxExemptVehicles.Any(x => x.Type == vehicle.VehicleType);
            }

            return false;
        }

        private async Task<bool> IsTaxFreeDay(Guid cityId, DateTime taxOn)
        {
            if (taxOn.DayOfWeek == DayOfWeek.Saturday || taxOn.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            var taxFreeDaysOfCity = await _mediator.Send(new GetAllTaxFreeDaysByCityIdQuery()
            {
                CityId = cityId
            });

            if (taxFreeDaysOfCity is { Count: > 0 })
            {
                var dayBeforeTaxOn = new DateTime(taxOn.Year, taxOn.Month, taxOn.Day+1, 0, 0, 0);

                return taxFreeDaysOfCity.Any(x =>
                    x.StartTime <= taxOn && x.EndTime >= taxOn || (x.StartTime <= dayBeforeTaxOn &&
                                                                   x.EndTime >= dayBeforeTaxOn && x.IsPublicHoliday));
            }

            return false;
        }

        private async Task<double> CalculateTaxOfVehicle(Guid vehicleId, Guid cityId, DateTime taxOn)
        {
            double tax = 0;
            // Finding all tax which user has paid in last one hour
            var taxPaidByUser = await _mediator.Send(new GetAllCongestionTaxQuery()
            {
                data = new GetAllCongestionTaxRequestDTO()
                {
                    CityId = cityId,
                    VehicleId = vehicleId
                }
            });

            var taxOnTimeSpan = new TimeSpan(taxOn.Hour, taxOn.Minute,
                taxOn.Second);

            // Getting all tax schedule of city
            var taxSchedules = await _mediator.Send(new GetAllTaxSchedulesByCityIdQuery()
            {
                CityId = cityId
            });

            var taxSchedule =
                taxSchedules.FirstOrDefault(x => x.StartTime <= taxOnTimeSpan && x.EndTime >= taxOnTimeSpan);

            // Checking if tax amount is not zero for that schedule
            if (taxSchedule != null && taxSchedule.Amount != 0)
            {
                var taxPaidInLastHour = taxPaidByUser.Where(x => x.TaxOn >= taxOn.AddHours(-1)).Sum(x => x.Amount);

                if (taxPaidInLastHour < taxSchedule.Amount)
                {
                    tax = taxSchedule.Amount - taxPaidInLastHour;
                }
                else if (taxPaidInLastHour == 0)
                {
                    tax = taxSchedule.Amount;
                }

                // Checking if tax is exceeding the per day limit
                var allCities = await _mediator.Send(new GetAllCitiesQuery());
                var city = allCities.FirstOrDefault(x => x.Id == cityId);

                var dayStart = new DateTime(taxOn.Year, taxOn.Month,
                    taxOn.Day, 0, 0, 0);

                var taxPaidOnWholeDay = taxPaidByUser.Where(x => x.TaxOn >= dayStart && x.TaxOn <= dayStart.AddDays(1)).Sum(x => x.Amount);

                if (city != null && taxPaidOnWholeDay + tax > city.MaxTaxPerDay)
                {
                    tax = city.MaxTaxPerDay - taxPaidOnWholeDay;
                }
            }


            return tax;
        }
    }
}
