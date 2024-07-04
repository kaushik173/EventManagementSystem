namespace EventManagement.Web.Models
{
    public class Festival
    {
        public int FestivalId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Contribution> Contributions { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Media> MediaFiles { get; set; }
    }
}
