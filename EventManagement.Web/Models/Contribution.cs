namespace EventManagement.Web.Models
{
    public class Contribution
    {
        public int ContributionId { get; set; }
        public int? MemberId { get; set; }
        public Member Member { get; set; }
        public int? ContributorId { get; set; }
        public Contributor Contributor { get; set; }
        public int FestivalId { get; set; }
        public Festival Festival { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
