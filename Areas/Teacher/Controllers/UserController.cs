using LmsProject.Data;
using LmsProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string viewMode = "card")
        {
            var user = await _userManager.GetUserAsync(User);

            // Example: Get user ID
            var teacherId = _context.Teachers
                            .Where(t => t.UserId == user!.Id)
                            .Select(t => t.Id)
                            .FirstOrDefault(); // returns int (default 0 if not found);
            var courses = _context.Courses.Where(c => c.TeacherId == teacherId);
            ViewBag.ViewMode = viewMode;
            return View(courses);
        }
    }
}
