using EventManagement.Services.FestivalService;
using EventManagement.Services.MemberService.MemberService;
using EventManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Web.Controllers
{
    public class ContributionController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IFestivalService _festivalService;

        public ContributionController(IMemberService memberService, IFestivalService festivalService)
        {
            _memberService = memberService;
            _festivalService = festivalService;
        }

        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            var festivals = _festivalService.GetAllFestivals();

            var viewModel = new CreateContributionViewModel
            {
                Members = members,
                Festivals = festivals
            };

            return View(viewModel);
        }
    }
}
