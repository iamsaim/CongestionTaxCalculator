using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/taxexemptvehicle")]
    public class TaxExemptVehicleController : ControllerBase
    {
        private readonly ITaxExemptVehicleService _taxExemptVehicleService;

        public TaxExemptVehicleController(ITaxExemptVehicleService taxExemptVehicleService)
        {
            _taxExemptVehicleService = taxExemptVehicleService;
        }

        [Route("All/{cityId}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<GetTaxExemptVehicleDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTaxExemptVehicle(Guid cityId)
        {
            var list = await _taxExemptVehicleService.GetAllTaxExemptVehicle(cityId);
            return Ok(list);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertTaxExemptVehicle(InsertTaxExemptVehicleDTO taxExemptVehicle)
        {
            var response = await _taxExemptVehicleService.InsertTaxExemptVehicle(taxExemptVehicle);
            return Ok(response);
        }

        [Route("")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTaxExemptVehicle(UpdateTaxExemptVehicleDTO taxExemptVehicle)
        {
            var response = await _taxExemptVehicleService.UpdateTaxExemptVehicle(taxExemptVehicle);
            return Ok(response);
        }

        [Route("")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleDeleteTaxExemptVehicle(ToggleDeleteDTO dto)
        {
            var response = await _taxExemptVehicleService.ToggleDeleteTaxExemptVehicle(dto);
            return Ok(response);
        }
    }
}
