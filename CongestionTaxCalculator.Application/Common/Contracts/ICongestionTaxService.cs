using CongestionTaxCalculator.Application.Common.Dto;
using CongestionTaxCalculator.Application.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Contracts
{
    public interface ICongestionTaxService
    {
        Task<List<GetAllCongestionTaxResponseDTO>> GetAllCongestionTax(GetAllCongestionTaxRequestDTO data);
        Task<GenericResponse<bool>> InsertCongestionTax(InsertCongestionTaxDTO request);
        Task<GenericResponse<bool>> UpdateCongestionTax(UpdateCongestionTaxDTO request);
        Task<GenericResponse<bool>> ToggleDeleteCongestionTax(ToggleDeleteDTO request);
    }
}
