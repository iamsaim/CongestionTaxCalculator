using CongestionTaxCalculator.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class GetAllVehiclesDTO
    {
        public Guid Id { get; set; }
        public string LicenceNumber { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
