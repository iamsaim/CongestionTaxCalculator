using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class GetAllCongestionTaxRequestDTO
    {   
            public Guid CityId { get; set; }
            public Guid VehicleId { get; set; }
        
    }
}
