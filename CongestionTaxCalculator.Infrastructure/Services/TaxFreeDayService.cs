using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using CongestionTaxCalculator.Application.Features.TaxFreeDay.Command;
using CongestionTaxCalculator.Application.Features.TaxFreeDay.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Infrastructure.Services
{
    public class TaxFreeDayService : ITaxFreeDayService
    {
        private readonly ILogger<TaxFreeDayService> _logger;
        private readonly IMediator _mediator;

        public TaxFreeDayService(ILogger<TaxFreeDayService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<List<GetAllTaxFreeDaysDTO>> GetAllTaxFreeDay(Guid CityId)
        {
            var response = await _mediator.Send(new GetAllTaxFreeDaysByCityIdQuery()
            {
                CityId = CityId
            });

            return response;
        }

        public async Task<GenericResponse<bool>> InsertTaxFreeDay(InsertTaxFreeDayDTO request)
        {
            var response = await _mediator.Send(new InsertTaxFreeDayCommand()
            {
                data = request
            });

            var message = "Tax free day added successfully";

            if (!response)
            {
                message = "Failed to insert Tax free day";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> UpdateTaxFreeDay(UpdateTaxFreeDayDTO request)
        {
            var response = await _mediator.Send(new UpdateTaxFreeDayCommand()
            {
                data = request
            });

            var message = "Tax free day updated successfully";

            if (!response)
            {
                message = "Failed to update Tax free day";
            }

            return new GenericResponse<bool>(response, message);
        }

        public async Task<GenericResponse<bool>> ToggleDeleteTaxFreeDay(ToggleDeleteDTO request)
        {
            var response = await _mediator.Send(new ToggleDeleteTaxFreeDayCommand()
            {
                data = request
            });

            var message = "Tax free day deleted successfully";

            if (!response)
            {
                message = "Failed to deleted Tax free day";
            }

            return new GenericResponse<bool>(response, message);
        }
    }
}
