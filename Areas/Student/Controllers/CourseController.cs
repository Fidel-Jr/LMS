using AutoMapper;
using LmsProject.Areas.Student.Models;
using LmsProject.Areas.Teacher.Models;
using LmsProject.Data;
using LmsProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LmsProject.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    [Area("Student")]
    public class CourseController : Controller
    {
        private const string Pending = "Pending";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;

        }

        private int GetCurrentStudentId()
        {
            var studentUserId = _userManager.GetUserId(User);

            return _context.Students
                .Where(s => s.UserId == studentUserId)
                .Select(s => s.Id)
                .SingleOrDefault();
        }

        public IActionResult Index(int id)
        {
            var studentId = GetCurrentStudentId();

            var studentCourseEnrollmentStatus = _context.EnrollmentRequests
                                                .Where(er => er.StudentId == studentId && er.CourseId == id)
                                                .Select(er=>er.Status).FirstOrDefault();

            var courseWithModules = _context.Courses
            .Where(c => c.Id == id)
            .Include(c => c.Modules)
                .ThenInclude(m => m.Materials)
            .Select(c => new CourseModuleMaterial
            {
                Course = c,
                Modules = c.Modules.ToList(),
                EnrollmentStatus = studentCourseEnrollmentStatus ?? "Not Enrolled"
            })
            .FirstOrDefault();

                return View(courseWithModules); // Pass the course data to the view
        }

        public IActionResult AllCourses()
        {
            var courses = _context.Courses
                .ToList(); // Get all courses for the student

            return View(new EnrolledCourse
            {
                Courses = courses
            });
        }
        
        public async Task<IActionResult> Enroll(int id)
        {
                
            var studentId = GetCurrentStudentId();

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound(); // course doesn't exist
            }

            // Check if already enrolled or already requested
            var existingRequest = await _context.EnrollmentRequests
                .FirstOrDefaultAsync(er => er.StudentId == studentId && er.CourseId == id);

            if (existingRequest != null)
            {
                // Option 1: Ignore silently
                // Option 2: Redirect with a message
                // Option 3: Update status if needed (ex: reset to Pending)
                return RedirectToAction("Index", "Course", new { id });
            }   

            var enrolmentRequest = new EnrollmentRequest
            {
                StudentId = studentId,
                CourseId = id,
                Status = Pending
            };
            _context.EnrollmentRequests.Add(enrolmentRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Course", new { id = id });

        }

    }
}
