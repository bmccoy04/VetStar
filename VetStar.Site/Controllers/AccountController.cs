using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetStar.Site.Models;
using System.Security.Claims;

namespace VetStar.Site.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel ln)
        {
            if (ln.UserName == ln.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim("Read", "true"),
                    new Claim(ClaimTypes.Name, "ayayalar"),
                    new Claim(ClaimTypes.Sid, "12345")
                };

                var claimsIdentity = new ClaimsIdentity(claims, "password");
                var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                HttpContext.Authentication.SignInAsync("Cookies", claimsPrinciple);

                return Redirect("~/");
            }

            return View(new LoginViewModel() { UserName = "Bryan" });
        }

        public IActionResult LogOut()
        {
            HttpContext.Authentication.SignOutAsync("Cookies");
            return Redirect("~/");
        }
    }
}