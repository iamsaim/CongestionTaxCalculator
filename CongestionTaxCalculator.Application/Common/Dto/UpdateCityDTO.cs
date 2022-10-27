using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class UpdateCityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double MaxTaxPerDay { get; set; }
    }
}
