using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    [Area("Student")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
