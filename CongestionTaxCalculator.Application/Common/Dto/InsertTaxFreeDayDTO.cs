using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class InsertTaxFreeDayDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid CityId { get; set; }
        public bool IsPublicHoliday { get; set; }
    }
}
