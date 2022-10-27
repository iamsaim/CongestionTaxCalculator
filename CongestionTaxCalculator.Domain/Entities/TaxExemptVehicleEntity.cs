using CongestionTaxCalculator.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongestionTaxCalculator.Domain.Entities
{
    [Table("TaxExemptVehicle")]
    public class TaxExemptVehicleEntity : BaseEntity
    {
        public VehicleType Type { get; set; }
        public Guid CityId { get; set; }
        public virtual CityEntity City { get; set; }
    }
}
