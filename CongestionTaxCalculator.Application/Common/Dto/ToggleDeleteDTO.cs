using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class ToggleDeleteDTO
    {
        public Guid Id { get; set; }
        public bool Delete { get; set; }
    }
}
