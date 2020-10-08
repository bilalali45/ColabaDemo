namespace Rainmaker.Model
{
    public class Sitemap
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsParent { get; set; }
        public bool? IsExecutable { get; set; } 
        public int? ParentId { get; set; }
        public string IconClass { get; set; } 
        public int DisplayOrder { get; set; }
        public bool IsPermissive { get; set; }
    }
}
