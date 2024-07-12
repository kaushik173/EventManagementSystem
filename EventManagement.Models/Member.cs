namespace EventManagement.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contribution> Contributions { get; set; }
    }
}
