namespace EventManagement.Web.Models
{
    public class Media
    {
        public int MediaId { get; set; }
        public int FestivalId { get; set; }
        public Festival Festival { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}
