using System.ComponentModel.DataAnnotations.Schema;

namespace CongestionTaxCalculator.Domain.Entities
{
    [Table("TaxSchedule")]
    public class TaxScheduleEntity : BaseEntity
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Amount { get; set; }
        public Guid CityId { get; set; }
        public virtual CityEntity City { get; set; }
    }
}
