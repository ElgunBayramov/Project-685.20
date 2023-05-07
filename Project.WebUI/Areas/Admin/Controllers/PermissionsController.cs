using Microsoft.AspNetCore.Mvc;

namespace Project.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
