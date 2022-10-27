using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Contracts
{
    public interface ITaxScheduleService
    {
        Task<List<GetAllTaxSchedulesDTO>> GetAllTaxSchedule(Guid CityId);
        Task<GenericResponse<bool>> InsertTaxSchedule(InsertTaxScheduleDTO request);
        Task<GenericResponse<bool>> UpdateTaxSchedule(UpdateTaxScheduleDTO request);
        Task<GenericResponse<bool>> ToggleDeleteTaxSchedule(ToggleDeleteDTO request);
    }
}
