namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class TriggerValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public string ChangeType { get; set; }
        public int? ChangeField { get; set; }
        public int? RuleId { get; set; }
        public bool IsDeleted { get; set; }
        public int EntityTypeId { get; set; }

        public Rule Rule { get; set; }
    }
}