using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Contracts
{
    public interface ITaxFreeDayService
    {
        Task<List<GetAllTaxFreeDaysDTO>> GetAllTaxFreeDay(Guid CityId);
        Task<GenericResponse<bool>> InsertTaxFreeDay(InsertTaxFreeDayDTO request);
        Task<GenericResponse<bool>> UpdateTaxFreeDay(UpdateTaxFreeDayDTO request);
        Task<GenericResponse<bool>> ToggleDeleteTaxFreeDay(ToggleDeleteDTO request);
    }
}
