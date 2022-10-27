using CongestionTaxCalculator.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class GetTaxExemptVehicleDTO
    {
        public Guid Id { get; set; }
        public VehicleType Type { get; set; }
        public Guid CityId { get; set; }
    }
}
