using CongestionTaxCalculator.Application.Common.Contracts;
using CongestionTaxCalculator.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/taxSchedule")]
    public class TaxScheduleController : ControllerBase
    {
        private readonly ITaxScheduleService _taxScheduleService;

        public TaxScheduleController(ITaxScheduleService taxScheduleService)
        {
            _taxScheduleService = taxScheduleService;
        }

        [Route("All/{cityId}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllTaxSchedulesDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTaxSchedule(Guid cityId)
        {
            var list = await _taxScheduleService.GetAllTaxSchedule(cityId);
            return Ok(list);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertTaxSchedule(InsertTaxScheduleDTO taxSchedule)
        {
            var response = await _taxScheduleService.InsertTaxSchedule(taxSchedule);
            return Ok(response);
        }

        [Route("")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdUpdateTaxScheduleateCity(UpdateTaxScheduleDTO taxSchedule)
        {
            var response = await _taxScheduleService.UpdateTaxSchedule(taxSchedule);
            return Ok(response);
        }

        [Route("")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ToggleDeleteTaxSchedule(ToggleDeleteDTO dto)
        {
            var response = await _taxScheduleService.ToggleDeleteTaxSchedule(dto);
            return Ok(response);
        }
    }
}
