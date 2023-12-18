using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using phoneBill.Data;
using phoneBill.Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace phoneBill.Controllers
{
    public class AuthenController : Controller
    {

        private readonly db_phonebillModel _db;
        public AuthenController(db_phonebillModel db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("th-TH")),
                        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
                    );

                return RedirectToAction(nameof(Index), "Home");
            }

            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var data = _db.VUserAuths.Where(s => s.Username == model.Username && s.Password == model.Password).FirstOrDefault();
                switch (data)
                {
                    case null :
                        TempData["Danger"] = "ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง";
                        break;
                    default:
                        await SetCookie(model,data);
                        return RedirectToAction(nameof(Index), "Home");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index), "Home");
        }

        private async Task SetCookie(LoginRequest model, VUserAuth data)
        {
            var props = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
            };

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, data.Fullname)
                    };


            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.NameIdentifier, ClaimsIdentity.DefaultRoleClaimType);
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        }



    }
}
