using EventManagement.Services.MemberService.MemberService;
using EventManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromBody] MemberViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _memberService.AddMember(model);
                    return Json(new { success = true, message = "Member created successfully." });
                }

                return Json(new { success = false, message = "Invalid data." });


            }
            catch (Exception)
            {
                return Json(new { success = false, error = "An error occurred while processing your request." });
            }
        }


        [HttpGet]
        public IActionResult GetAllMembers()
        {
            try
            {
                var members =  _memberService.GetAllMembers();
                return Json(members);
            }
            catch (Exception)
            {
                return Json(new { error = "An error occurred while fetching members." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] MemberViewModel memberViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data." });
            }

            try
            {
                bool changesMade = await _memberService.UpdateMember(memberViewModel);

                if (changesMade)
                {
                    return Json(new { success = true, changesMade = true, message = "Member updated successfully!." });
                }
                else
                {
                    return Json(new { success = true, changesMade = false, message = "No changes were made." });
                }
            }
            catch (KeyNotFoundException)
            {
                return Json(new { success = false, message = "Member not found." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _memberService.DeleteMember(id);

                return Json(new { success = true, message = "Member deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting member: {ex.Message}" });
            }
        }

    }
}
