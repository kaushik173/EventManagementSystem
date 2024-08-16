using EventManagement.Models;
using EventManagement.Repositories;
using EventManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Services.FestivalService
{
    public class FestivalService : IFestivalService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FestivalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddFestival(FestivalViewModel festival)
        {
            var festivalModel = festival.ConvertToModel();
            await _unitOfWork.GenericRepository<Festival>().AddAsync(festivalModel);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<Festival> GetAllFestivals()
        {
            return _unitOfWork.GenericRepository<Festival>().GetAllAsync();
        }
    }
}
