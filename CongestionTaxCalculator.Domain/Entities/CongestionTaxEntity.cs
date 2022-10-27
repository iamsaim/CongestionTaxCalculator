using System.ComponentModel.DataAnnotations.Schema;

namespace CongestionTaxCalculator.Domain.Entities
{
    [Table("CongestionTax")]
    public class CongestionTaxEntity : BaseEntity
    {
        public DateTime TaxOn { get; set; }
        public double Amount { get; set; }
        public Guid CityId { get; set; }
        public virtual CityEntity City { get; set; }
        public Guid VehicleId { get; set; }
        public virtual VehicleEntity Vehicle { get; set; }

    }
}
