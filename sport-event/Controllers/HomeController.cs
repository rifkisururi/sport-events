using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sport_event.Models;
using sport_event.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace sport_event.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userLogin = GetUserLogin();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public UserClaimModel GetUserLogin()
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