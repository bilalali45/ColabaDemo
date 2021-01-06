namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class NoteDepartmentBinder
    {
        public int DepartmentId { get; set; }
        public int NoteId { get; set; }

        public Department Department { get; set; }

        public Note Note { get; set; }
    }
}