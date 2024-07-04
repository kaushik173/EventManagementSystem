namespace EventManagement.Web.Models
{
    public class Contributor
    {
        public int ContributorId { get; set; }
        public string Name { get; set; }
        public ICollection<Contribution> Contributions { get; set; }
    }
}
