using EventManagement.Models;

namespace EventManagement.ViewModels
{
    public class FestivalViewModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Festival ConvertToModel()
        {
            return new Festival
            {
                Name = this.Name,
                Date = this.Date
            };
        }
    }

}
