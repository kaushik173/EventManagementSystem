using EventManagement.Models;
using EventManagement.ViewModels;

namespace EventManagement.Services.FestivalService
{
    public interface IFestivalService
    {
        Task AddFestival(FestivalViewModel viewModel);
        IEnumerable<Festival> GetAllFestivals();
    }
}
