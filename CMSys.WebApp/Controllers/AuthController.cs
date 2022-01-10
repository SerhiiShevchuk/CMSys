using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CMSys.Core.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CMSys.WebApp.Controllers
{
    public class AuthController : Controller
    {
        IUnitOfWork _uow;
        public AuthController(IUnitOfWork uow) => _uow = uow;

        [Route("[action]")]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "Email cannot be empty");
            }
            else if (email.Length < 4 || email.Length > 128)
            {
                ModelState.AddModelError("email", "Email's length must be [4..128]");
            }

            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "Password cannot be empty");
            }
            else if (password.Length < 5 || password.Length > 128)
            {
                ModelState.AddModelError("password", "Password's length must be [5..128]");
            }

            if (!ModelState.IsValid)
            {
                return PartialView();
            }

            var user = _uow.UserRepository.FindByEmail(email);
            if (user == null || !user.VerifyPassword(password))
            {
                ModelState.AddModelError("password", "Invalid password");

                return PartialView();
            }

            var principal = new ClaimsPrincipal(new ClaimsIdentity(user.GetClaims(), "Cookie"));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties { AllowRefresh = true });

            return RedirectToAction("Courses", "Courses"); ;
        }
    }
}
