using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LearnAuth.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace LearnAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(ILogger<HomeController> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Policy ="Claim.Dob")]
        public IActionResult PrivacyPolicy()
        {
            return View("Privacy");
        }

        public async Task<IActionResult> DoAuth()
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, "Claim.Dob");
            if (authResult.Succeeded)
            {
                return View("Index");
            }
            return View("Index");
        }
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email,"Bob@gmail.com"),
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimTypes.DateOfBirth, "18/11/96")
            };
            var identity = new ClaimsIdentity(claims,"bobIdentity");
            var userPrincipal = new ClaimsPrincipal(new[] { identity });
            await HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
