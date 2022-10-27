using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{

    public class GetAllCongestionTaxResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime TaxOn { get; set; }
        public double Amount { get; set; }
        public Guid CityId { get; set; }
        public Guid VehicleId { get; set; }
    }
}
