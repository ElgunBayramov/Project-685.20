using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.WebUI.Business.AccountModule;
using Project.WebUI.Models.DataContexts;
using Project.WebUI.Models.Entities.Membership;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Project.WebUI.AppCode.Extensions;
using System.Security.Claims;
using Project.WebUI.Business.ProfessionModule;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.WebUI.Business.DepartmentModule;

namespace Project.WebUI.Controllers
{
    [Authorize(Roles = "user")]
    public class HomeController : Controller
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IMediator mediator;

        public HomeController(IMediator mediator, ProjectDbContext dbContext)
        {
            this.mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = User;
            var user = new ProjectUser();

            if (currentUser.Identity.IsAuthenticated)
            {
                string userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
                int.TryParse(userId, out int UserId);
               user = _dbContext.Users.Find(UserId);
            }
            ViewBag.professionName = _dbContext.Professions
       .Where(p => p.Id == user.ProfessionId)
       .Select(x => x.Name)
       .First();
            ViewBag.departmentName = _dbContext.Departments
       .Where(p => p.Id == user.DepartmentId)
       .Select(x => x.Name)
       .First();

            return View(user);


            //var model = new MultiHomeCommand
            //{
            //    User = new ProjectUser(),
            //    UserIsNullDescription = string.Empty,
            //};


            //if (userId != 0)
            //    model.User = _dbContext.Users
            //.Where(u => u.Id == userId)
            //.First();

            //if (userId == 0)

            //    model.UserIsNullDescription = "user is null";


            //    return View(model);
        }
        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }

    }
}
