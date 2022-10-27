using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;

namespace CongestionTaxCalculator.Application.Common.Contracts;

public interface IVehicleService
{
    Task<List<GetAllVehiclesDTO>> GetAllVehicles();
    Task<GenericResponse<bool>> InsertVehicle(InsertVehicleDTO request);
    Task<GenericResponse<bool>> UpdateVehicle(UpdateVehicleDTO request);
    Task<GenericResponse<bool>> ToggleDeleteVehicle(ToggleDeleteDTO request);

}