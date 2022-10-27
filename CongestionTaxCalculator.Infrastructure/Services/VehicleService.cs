using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using CongestionTaxCalculator.Application.Features.Vehicle.Command;
using CongestionTaxCalculator.Application.Features.Vehicle.Query;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CongestionTaxCalculator.Infrastructure.Services;

public class VehicleService: IVehicleService
{
    private readonly ILogger<VehicleService> _logger;
    private readonly IMediator _mediator;

    public VehicleService(ILogger<VehicleService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    public async Task<List<GetAllVehiclesDTO>> GetAllVehicles()
    {
        var response = await _mediator.Send(new GetAllVehiclesQuery()
        {
        });

        return response;
    }

    public async Task<GenericResponse<bool>> InsertVehicle(InsertVehicleDTO request)
    {
        var response = await _mediator.Send(new InsertVehicleCommand()
        {
            data = request
        });

        var message = "Vehicle added successfully";

        if (!response)
        {
            message = "Failed to insert vehicle";
        }

        return new GenericResponse<bool>(response, message);
    }

    public async Task<GenericResponse<bool>> UpdateVehicle(UpdateVehicleDTO request)
    {
        var response = await _mediator.Send(new UpdateVehicleCommand()
        {
            data = request
        });

        var message = "Vehicle updated successfully";

        if (!response)
        {
            message = "Failed to update Vehicle";
        }

        return new GenericResponse<bool>(response, message);
    }

    public async Task<GenericResponse<bool>> ToggleDeleteVehicle(ToggleDeleteDTO request)
    {
        var response = await _mediator.Send(new ToggleDeleteVehicleCommand()
        {
            data = request
        });

        var message = "Vehicle deleted successfully";

        if (!response)
        {
            message = "Failed to deleted Vehicle";
        }

        return new GenericResponse<bool>(response, message);
    }

}