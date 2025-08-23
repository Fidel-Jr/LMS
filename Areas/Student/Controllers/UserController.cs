using AutoMapper;
using LmsProject.Areas.Student.Models;
using LmsProject.Data;
using LmsProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LmsProject.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    [Area("Student")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;

        }
        
        public async Task<IActionResult> Index(string viewMode = "card")
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);
            var courses = _context.StudentCourses.Where(sc => sc.Student.UserId == userId)
                .Select(sc => sc.Course)
                .ToList();
            ViewBag.ViewMode = viewMode;
            return View(new EnrolledCourse
            {
                Courses = courses
            });
        }
    }
}
