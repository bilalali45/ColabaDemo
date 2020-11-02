namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class PaymentOn
    {
        public int Id { get; set; }
        public int? Month { get; set; }
        public int? NoOfMonths { get; set; }
        public int? PropertyTaxId { get; set; }

        public PropertyTax PropertyTax { get; set; }
    }
}