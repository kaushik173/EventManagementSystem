namespace EventManagement.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int FestivalId { get; set; }
        public Festival Festival { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
