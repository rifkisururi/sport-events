using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sport_event.Services;
using sport_event.ViewModels;
using sport_event.ViewModels.Auth;
using sport_event.ViewModels.Organizer;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sport_event.Controllers
{
    [Authorize]
    public class OrganizerController : Controller
    {
        private UserService _userService;
        private OrganizerService _orgService;
        public OrganizerController()
        {
            _userService = new UserService();
            _orgService = new OrganizerService();
        }
        public IActionResult Index()
        {
            GetUserLogin();
            return View();
        }

        public async Task<IActionResult> getData(int page = 1, int perPage = 10)
        {
            var user = GetUserLogin();

            var data = await _orgService.getData(user.token, page, perPage);
            return new JsonResult(new
            {
                Draw = perPage,
                RecordsTotal = data.Meta.Pagination.Total,
                RecordsFiltered = data.Meta.Pagination.Count,
                Data = data.Data
            });
        }

        public async Task<IActionResult> removeData(int id)
        {
            var user = GetUserLogin();

            var data = await _orgService.DeleteData(user.token, id);
            return new JsonResult(new
            {
                status = data
            });
        }

        public async Task<IActionResult> addData([FromBody] OrganizerDto data)
        {
            var user = GetUserLogin();

            var result = await _orgService.createData(user.token, data);
            return new JsonResult(new
            {
                result = result
            });
        }

        public async Task<IActionResult> updateData([FromBody] OrganizerDto data)
        {
            var user = GetUserLogin();

            var result = await _orgService.UpdateData(user.token, data);
            return new JsonResult(new
            {
                result = result
            });
        }

        private UserClaimModel GetUserLogin()
        {
            var data = new UserClaimModel();
            data.token = User.FindFirstValue("token");
            data.name = User.FindFirstValue(ClaimTypes.Name);
            data.id = Convert.ToInt32(User.FindFirstValue("id"));
            data.email = User.FindFirstValue("email");

            ViewBag.FullName = data.name;
            ViewBag.Email = data.email;
            return data;
        }
    }
}
