using EventManagement.Models;

namespace EventManagement.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static MemberViewModel FromModel(Member member)
        {
            return new MemberViewModel
            {
                Id = member.Id,
                Name = member.Name
            };
        }

        public Member ConvertToModel()
        {
            return new Member
            {
                Name = this.Name
            };
        }
    }
}
