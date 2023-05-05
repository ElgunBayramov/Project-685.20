using Microsoft.AspNetCore.Mvc;

namespace Project.WebUI.Areas.Admin.Controllers
{
    public class PermissionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
