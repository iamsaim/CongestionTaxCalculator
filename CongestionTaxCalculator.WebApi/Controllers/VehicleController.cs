using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.WebApi.Controllers;


[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/vehicle")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [Route("All")]
    [HttpGet]
    [ProducesResponseType(typeof(List<GetAllVehiclesDTO>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehiclesList = await _vehicleService.GetAllVehicles();
        return Ok(vehiclesList);
    }

    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> InsertVehicle(InsertVehicleDTO vehicle)
    {
        var response = await _vehicleService.InsertVehicle(vehicle);
        return Ok(response);
    }

    [Route("")]
    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateVehicle(UpdateVehicleDTO vehicle)
    {
        var response = await _vehicleService.UpdateVehicle(vehicle);
        return Ok(response);
    }

    [Route("")]
    [HttpDelete]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> ToggleDeleteVehicle(ToggleDeleteDTO dto)
    {
        var response = await _vehicleService.ToggleDeleteVehicle(dto);
        return Ok(response);
    }
}