using EventManagement.Models;
using EventManagement.ViewModels;

namespace EventManagement.Services.MemberService.MemberService
{
    public interface IMemberService
    {
        Task AddMember(MemberViewModel viewModel);
        IEnumerable<Member> GetAllMembers();
        Task<bool> UpdateMember(MemberViewModel memberViewModel);
        Task<MemberViewModel> GetById(int id);
        Task DeleteMember(int id);
        Task<int> GetTotalMembersAsync();
    }
}
