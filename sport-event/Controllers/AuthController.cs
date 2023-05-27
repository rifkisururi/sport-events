using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using sport_event.Services;
using sport_event.ViewModels.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace sport_event.Controllers
{
    public class AuthController : Controller
    {
        private UserService _authService; 
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILogger<AuthController> logger)
        {
            _authService = new UserService();
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginData)
        {
            _logger.LogInformation($"Login start with email {loginData.email}");
            var result = await _authService.Login(loginData);
            var status = false;
            if (!string.IsNullOrEmpty(result.token)){
                status = true;
                string token = result.token;                
                // get detail data
                var detailUser = await _authService.getDetailUser(result.id, result.token);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, detailUser.firstName + " " + detailUser.lastName),
                    new Claim(ClaimTypes.Email, detailUser.email),
                    new Claim("id", detailUser.id.ToString()),
                    new Claim("email", result.email),
                    new Claim("token", result.token)
                    // Add more claims as needed
                };

                var identity = new ClaimsIdentity(claims, "APIAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);
            }

            return new JsonResult(new
            {
                result = result,
                status = status
            });
        }

        [HttpGet]
        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> register([FromBody] UserModel data)
        {
            var result = await _authService.createUser(data);
            return new JsonResult(new
            {
                result = result
            });
        }

        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
