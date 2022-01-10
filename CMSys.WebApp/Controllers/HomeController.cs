using Microsoft.AspNetCore.Mvc;

namespace CMSys.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index() => RedirectToAction("Login", "Auth");
    }
}
