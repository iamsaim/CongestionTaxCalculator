using System.ComponentModel.DataAnnotations.Schema;

namespace CongestionTaxCalculator.Domain.Entities
{
    [Table("City")]
    public class CityEntity : BaseEntity
    { 
        public string Name { get; set; }
        public double MaxTaxPerDay { get; set; }
        public virtual ICollection<TaxExemptVehicleEntity> Exemptions { get; set; }
        public virtual ICollection<CongestionTaxEntity> Taxes { get; set; }
        public virtual ICollection<TaxScheduleEntity> TaxeSchedules { get; set; }
        public virtual ICollection<TaxFreeDayEntity> TaxFreeDays { get; set; }

    }
}
