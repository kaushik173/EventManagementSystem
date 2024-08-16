using EventManagement.Services.FestivalService;
using EventManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Web.Controllers
{
    public class FestivalController : Controller
    {
        private readonly IFestivalService _festivalService;
        public FestivalController(IFestivalService festivalService)
        {
            _festivalService = festivalService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromBody] FestivalViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _festivalService.AddFestival(model);
                    return Json(new { success = true, message = "Festival saved successfully." });
                }

                return Json(new { success = false, message = "Invalid data." });


            }
            catch (Exception)
            {
                return Json(new { success = false, error = "An error occurred while processing your request." });
            }
        }


        [HttpGet]
        public IActionResult GetAllFestivals()
        {
            try
            {
                var festivals = _festivalService.GetAllFestivals();
                return Json(festivals);
            }
            catch (Exception)
            {
                return Json(new { error = "An error occurred while fetching members." });
            }
        }
    }
}
