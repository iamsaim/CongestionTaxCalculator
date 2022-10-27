using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/city")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Route("All")]
        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllCitiesDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCities()
        {
            var list = await _cityService.GetAllCities();
            return Ok(list);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertCity(InsertCityDTO city)
        {
            var response = await _cityService.InsertCity(city);
            return Ok(response);
        }

        [Route("")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCity(UpdateCityDTO city)
        {
            var response = await _cityService.UpdateCity(city);
            return Ok(response);
        }

        [Route("")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleDeleteCity(ToggleDeleteDTO dto)
        {
            var response = await _cityService.ToggleDeleteCity(dto);
            return Ok(response);
        }
    }
}
