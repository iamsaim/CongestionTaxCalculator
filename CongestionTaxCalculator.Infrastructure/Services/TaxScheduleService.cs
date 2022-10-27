using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using CongestionTaxCalculator.Application.Features.TaxSchedule.Command;
using CongestionTaxCalculator.Application.Features.TaxSchedule.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Infrastructure.Services
{
    public class TaxScheduleService : ITaxScheduleService
    {
        private readonly ILogger<TaxScheduleService> _logger;
        private readonly IMediator _mediator;

        public TaxScheduleService(ILogger<TaxScheduleService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<List<GetAllTaxSchedulesDTO>> GetAllTaxSchedule(Guid CityId)
        {
            var response = await _mediator.Send(new GetAllTaxSchedulesByCityIdQuery()
            {
                CityId = CityId
            });

            return response;
        }

        public async Task<GenericResponse<bool>> InsertTaxSchedule(InsertTaxScheduleDTO request)
        {
            var response = await _mediator.Send(new InsertTaxScheduleCommand()
            {
                data = request
            });

            var message = "Tax schedule added successfully";

            if (!response)
            {
                message = "Failed to insert Tax schedule";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> UpdateTaxSchedule(UpdateTaxScheduleDTO request)
        {
            var response = await _mediator.Send(new UpdateTaxScheduleCommand()
            {
                data = request
            });

            var message = "Tax schedule updated successfully";

            if (!response)
            {
                message = "Failed to update Tax schedule";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> ToggleDeleteTaxSchedule(ToggleDeleteDTO request)
        {
            var response = await _mediator.Send(new ToggleDeleteTaxScheduleCommand()
            {
                data = request
            });

            var message = "Tax schedule deleted successfully";

            if (!response)
            {
                message = "Failed to deleted Tax schedule";
            }

            return new GenericResponse<bool>(response, message);
        }
    }
}
