using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Command;
using CongestionTaxCalculator.Application.Features.TaxExemptVehicle.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Infrastructure.Services
{
    public class TaxExemptVehicleService : ITaxExemptVehicleService
    {
        private readonly ILogger<TaxExemptVehicleService> _logger;
        private readonly IMediator _mediator;

        public TaxExemptVehicleService(ILogger<TaxExemptVehicleService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<List<GetTaxExemptVehicleDTO>> GetAllTaxExemptVehicle(Guid CityId)
        {
            var response = await _mediator.Send(new GetTaxExemptVehicleByCityIdQuery()
            {
                CityId = CityId
            });

            return response;
        }

        public async Task<GenericResponse<bool>> InsertTaxExemptVehicle(InsertTaxExemptVehicleDTO request)
        {
            var response = await _mediator.Send(new InsertTaxExemptVehicleCommand()
            {
                data = request
            });

            var message = "Tax exempt vehicle added successfully";

            if (!response)
            {
                message = "Failed to insert tax exempt vehicle";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> UpdateTaxExemptVehicle(UpdateTaxExemptVehicleDTO request)
        {
            var response = await _mediator.Send(new UpdateTaxExemptVehicleCommand()
            {
                data = request
            });

            var message = "Tax exempt vehicle updated successfully";

            if (!response)
            {
                message = "Failed to update Tax exempt vehicle";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> ToggleDeleteTaxExemptVehicle(ToggleDeleteDTO request)
        {
            var response = await _mediator.Send(new ToggleDeleteTaxExemptVehicleCommand()
            {
                data = request
            });

            var message = "Tax exempt vehicle deleted successfully";

            if (!response)
            {
                message = "Failed to deleted Tax exempt vehicle";
            }

            return new GenericResponse<bool>(response, message);
        }
    }
}
