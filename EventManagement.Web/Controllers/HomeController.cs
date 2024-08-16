using EventManagement.Services.MemberService.MemberService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventManagement.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMemberService _memberService;

        public HomeController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> Index()
        {
            var totalMembers = await _memberService.GetTotalMembersAsync();
            ViewBag.TotalMembers = totalMembers;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
