using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sport_event.Services;
using sport_event.ViewModels;
using sport_event.ViewModels.Auth;
using System.Security.Claims;

namespace sport_event.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }
        public async Task<IActionResult> Index()
        {
            var curentUser = GetUserLogin();
            var detailUser = await _userService.getDetailUser(Convert.ToInt32(curentUser.id), curentUser.token);

            ViewBag.detailUser = detailUser;
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> updateData([FromBody] UserModel data) {
            var curentUser = GetUserLogin();
            data.id = curentUser.id;
            var statusUpdate = _userService.UpdateDetailUser(curentUser.token,data);

            return new JsonResult(new
            {
                status = statusUpdate
            });
        }

        [HttpPost]
        public async Task<IActionResult> updatePassword([FromBody] UserModel data)
        {
            var curentUser = GetUserLogin();
            data.id = curentUser.id;
            var status = await _userService.UpdatePasswordUser(curentUser.token, data);

            return new JsonResult(new
            {
                status = status
            });
        }

        public async Task<IActionResult> delete()
        {
            var curentUser = GetUserLogin();
            var status = _userService.deletelUser(curentUser.token, curentUser.id);

            return new JsonResult(new
            {
                status = status
            });
        }

        private UserClaimModel GetUserLogin()
        {
            var data = new UserClaimModel();
            data.token = User.FindFirstValue("token");
            data.name = User.FindFirstValue(ClaimTypes.Name);
            data.id =  Convert.ToInt32(User.FindFirstValue("id"));
            data.email = User.FindFirstValue("email");

            ViewBag.FullName = data.name;
            ViewBag.Email = data.email;
            return data;
        }

    }

    
}
