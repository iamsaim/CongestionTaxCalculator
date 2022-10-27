using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/taxfreeday")]
    public class TaxFreeDayController : ControllerBase
    {
        private readonly ITaxFreeDayService _taxFreeDayService;

        public TaxFreeDayController(ITaxFreeDayService taxFreeDayService)
        {
            _taxFreeDayService = taxFreeDayService;
        }

        [Route("All/{cityId}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllTaxFreeDaysDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTaxFreeDay(Guid cityId)
        {
            var list = await _taxFreeDayService.GetAllTaxFreeDay(cityId);
            return Ok(list);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertTaxFreeDay(InsertTaxFreeDayDTO taxFreeDay)
        {
            var response = await _taxFreeDayService.InsertTaxFreeDay(taxFreeDay);
            return Ok(response);
        }

        [Route("")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTaxFreeDay(UpdateTaxFreeDayDTO taxFreeDay)
        {
            var response = await _taxFreeDayService.UpdateTaxFreeDay(taxFreeDay);
            return Ok(response);
        }

        [Route("")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleDeleteTaxFreeDay(ToggleDeleteDTO dto)
        {
            var response = await _taxFreeDayService.ToggleDeleteTaxFreeDay(dto);
            return Ok(response);
        }
    }
}
