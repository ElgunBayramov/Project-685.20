using Microsoft.AspNetCore.Mvc;

namespace Project.WebUI.Controllers
{
    public class AccountController : Controller
    {
        [Route("/signin.html")]
        public IActionResult Signin()
        {
            return View();
        }
    }
}
