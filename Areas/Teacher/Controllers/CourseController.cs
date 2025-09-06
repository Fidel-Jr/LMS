using AutoMapper;
using LmsProject.Areas.Teacher.Models;
using LmsProject.Data;
using LmsProject.Models;
using LmsProject.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace LmsProject.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;

        }

        private int GetCurrentTeacherId()
        {
            var teacherUserId = _userManager.GetUserId(User);
            return _context.Teachers
                .Where(t => t.UserId == teacherUserId)
                .Select(t => t.Id)
                .SingleOrDefault();
        }

        public IActionResult Index(int id)
        {
            var course = _context.Courses
                        .Include(c => c.Modules)
                            .ThenInclude(m => m.Materials)
                        .FirstOrDefault(c => c.Id == id);

            var enrollmentRequests = _context.EnrollmentRequests
                .Where(er => er.CourseId == id && er.Status == "Pending").Include(er=>er.Student)
                                                                         .ThenInclude(Student => Student.User)
                                                                         .ToList();
            return View(new CourseModuleMaterial
            {
                Course = course!,
                EnrollmentRequest = enrollmentRequests
            });
        }
        
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title, Description, EnrollmentCount")] CourseDTO courseDto)
        {
            if (ModelState.IsValid)
            {   
                var teacherId = GetCurrentTeacherId();

                var course = _mapper.Map<Course>(courseDto);
                course.TeacherId = teacherId;
                _context.Add(course);
                await _context.SaveChangesAsync();  
                return RedirectToAction("Index", "Course");
            }
            return View(courseDto);
        }

        public IActionResult Manage()
        {
            var teacherId = GetCurrentTeacherId();

            var courses = _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToList();
            return View(courses);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(i => i.Id == id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Title, Description, EnrollmentCount")] CourseDTO courseDto)
        {

            var course = await _context.Courses.FindAsync(id);
            if (course is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(course);
            }

            _mapper.Map(courseDto, course);
            _context.Courses.Update(course);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> AcceptEnrollment(int id)
        {
            var enrollmentRequest = await _context.EnrollmentRequests.FindAsync(id);

            if(enrollmentRequest is null)
            {
                return NotFound();
            }

            enrollmentRequest.Status = "Accepted";

            _context.Update(enrollmentRequest);
            await _context.SaveChangesAsync();

            var studentCourse = new StudentCourse
            {
                StudentId = enrollmentRequest!.StudentId,
                CourseId = enrollmentRequest.CourseId
            };

            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = enrollmentRequest.CourseId });
        }

    }
}
