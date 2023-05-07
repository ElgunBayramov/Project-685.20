using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebUI.Controllers
{
    [Authorize(Roles ="user")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }

    }
}
