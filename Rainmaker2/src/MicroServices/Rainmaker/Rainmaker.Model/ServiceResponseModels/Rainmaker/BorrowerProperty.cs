namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BorrowerProperty
    {
        public int BorrowerId { get; set; }
        public int PropertyInfoId { get; set; }

        //public Borrower Borrower { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

    }
}