using System.ComponentModel.DataAnnotations.Schema;

namespace CongestionTaxCalculator.Domain.Entities
{
    [Table("TaxFreeDay")]
    public class TaxFreeDayEntity : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid CityId { get; set; }
        public bool IsPublicHoliday { get; set; }
        public virtual CityEntity City { get; set; }
    }
}
