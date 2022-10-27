using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;

namespace CongestionTaxCalculator.Application.Common.Contracts;

public interface ICityService
{
    Task<List<GetAllCitiesDTO>> GetAllCities();
    Task<GenericResponse<bool>> InsertCity(InsertCityDTO request);
    Task<GenericResponse<bool>> UpdateCity(UpdateCityDTO request);
    Task<GenericResponse<bool>> ToggleDeleteCity(ToggleDeleteDTO request);
}