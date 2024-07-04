using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Web.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
