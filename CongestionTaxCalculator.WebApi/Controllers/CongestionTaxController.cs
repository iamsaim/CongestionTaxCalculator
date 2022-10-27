using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/congestiontax")]
    public class CongestionTaxController : ControllerBase
    {
        private readonly ICongestionTaxService _congestionTaxService;

        public CongestionTaxController(ICongestionTaxService congestionTaxService)
        {
            _congestionTaxService = congestionTaxService;
        }

        [Route("All/{cityId}/{vehicleId}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllCongestionTaxResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCongestionTax(Guid cityId, Guid vehicleId)
        {
            var list = await _congestionTaxService.GetAllCongestionTax(new GetAllCongestionTaxRequestDTO()
            {
                CityId = cityId,
                VehicleId = vehicleId

            });
            return Ok(list);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertCongestionTax(InsertCongestionTaxDTO tax)
        {
            var response = await _congestionTaxService.InsertCongestionTax(tax);
            return Ok(response);
        }

        [Route("")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCongestionTax(UpdateCongestionTaxDTO tax)
        {
            var response = await _congestionTaxService.UpdateCongestionTax(tax);
            return Ok(response);
        }

        [Route("")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleDeleteCongestionTax(ToggleDeleteDTO dto)
        {
            var response = await _congestionTaxService.ToggleDeleteCongestionTax(dto);
            return Ok(response);
        }
    }
}
