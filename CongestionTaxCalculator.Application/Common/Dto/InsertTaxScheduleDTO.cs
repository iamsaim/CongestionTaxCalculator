using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class InsertTaxScheduleDTO
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Amount { get; set; }
        public Guid CityId { get; set; }
    }
}
