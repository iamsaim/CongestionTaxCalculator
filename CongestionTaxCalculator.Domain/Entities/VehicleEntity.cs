using CongestionTaxCalculator.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace CongestionTaxCalculator.Domain.Entities
{
    [Table("Vehicle")]
    public class VehicleEntity : BaseEntity
    {
        public string LicenceNumber { get; set; }
        public VehicleType VehicleType { get; set; }
        public virtual ICollection<CongestionTaxEntity> Taxes { get; set; }

    }
}
