using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Contracts
{
    public interface ITaxExemptVehicleService
    {
        Task<List<GetTaxExemptVehicleDTO>> GetAllTaxExemptVehicle(Guid CityId);
        Task<GenericResponse<bool>> InsertTaxExemptVehicle(InsertTaxExemptVehicleDTO request);
        Task<GenericResponse<bool>> UpdateTaxExemptVehicle(UpdateTaxExemptVehicleDTO request);
        Task<GenericResponse<bool>> ToggleDeleteTaxExemptVehicle(ToggleDeleteDTO request);
    }
}
