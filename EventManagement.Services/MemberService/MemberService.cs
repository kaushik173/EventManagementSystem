using EventManagement.Models;
using EventManagement.Repositories;
using EventManagement.ViewModels;

namespace EventManagement.Services.MemberService.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddMember(MemberViewModel member)
        {
            var model = member.ConvertToModel();
            await _unitOfWork.GenericRepository<Member>().AddAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _unitOfWork.GenericRepository<Member>().GetAllAsync();
        }

        public async Task<MemberViewModel> GetById(int id)
        {
            var member = await _unitOfWork.GenericRepository<Member>().GetByIdAsync(id);
            if (member == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }
            return MemberViewModel.FromModel(member);
        }

        public async Task<bool> UpdateMember(MemberViewModel memberViewModel)
        {
            var member = await _unitOfWork.GenericRepository<Member>().GetByIdAsync(memberViewModel.Id);
            if (member == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }

            if (member.Name != memberViewModel.Name)
            {
                member.Name = memberViewModel.Name;

                await _unitOfWork.GenericRepository<Member>().UpdateAsync(member);
                await _unitOfWork.SaveAsync();

                return true;
            }

            return false;
        }

        public async Task DeleteMember(int id)
        {
            var member = await _unitOfWork.GenericRepository<Member>().GetByIdAsync(id);
            if (member != null)
            {
                await _unitOfWork.GenericRepository<Member>().DeleteAsync(member);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new KeyNotFoundException("Member not found.");
            }
        }

        public async Task<int> GetTotalMembersAsync()
        {
            return await _unitOfWork.GenericRepository<Member>().CountAsync();
        }
    }
}
