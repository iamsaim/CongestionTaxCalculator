namespace CongestionTaxCalculator.Application.Common.Dto
{
    public class GetAllCitiesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double MaxTaxPerDay { get; set; }
    }
}
