using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using CongestionTaxCalculator.Application.Features.City.Command;
using CongestionTaxCalculator.Application.Features.City.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Infrastructure.Services;

public class CityService: ICityService
{
    private readonly ILogger<CityService> _logger;
    private readonly IMediator _mediator;

    public CityService(ILogger<CityService> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<List<GetAllCitiesDTO>> GetAllCities()
    {
        var response =  await _mediator.Send(new GetAllCitiesQuery()
        {
        });

        return response;
    }

    public async Task<GenericResponse<bool>> InsertCity(InsertCityDTO request)
    {
        var response = await _mediator.Send(new InsertCityCommand()
        {
            data = request
        });

        var message = "City added successfully";

        if (!response)
        {
            message = "Failed to insert city";
        }

        return new GenericResponse<bool>(response, message);
    }

    public async Task<GenericResponse<bool>> UpdateCity(UpdateCityDTO request)
    {
        var response = await _mediator.Send(new UpdateCityCommand()
        {
            data = request
        });

        var message = "City updated successfully";

        if (!response)
        {
            message = "Failed to update city";
        }

        return new GenericResponse<bool>(response, message);
    }

    public async Task<GenericResponse<bool>> ToggleDeleteCity(ToggleDeleteDTO request)
    {
        var response = await _mediator.Send(new ToggleDeleteCityCommand()
        {
            data = request
        });

        var message = "city deleted successfully";

        if (!response)
        {
            message = "Failed to deleted city";
        }

        return new GenericResponse<bool>(response, message);
    }
}