using AutoMapper;
using LmsProject.Data;
using LmsProject.Models;
using LmsProject.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace LmsProject.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    public class ModuleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public ModuleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
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

        public IActionResult Create(int id)
        {
            var course = _context.Courses.Where(c => c.Id == id)
                .Select(c=>c.Title)
                .FirstOrDefault();
            ViewData["Title"] = course;
            return View();
        }

        public async Task<IActionResult> Manage()
        {
            var teacherId = GetCurrentTeacherId();// returns int (default 0 if not found);
                                               // Get all courses for this teacher
            // Get all modules related to these courses
            var modules = await _context.Modules
                .Where(m => m.Course.TeacherId == teacherId)
                .ToListAsync();

            return View(modules);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, [Bind("Title, Description")] ModuleDTO moduleDto)
        {
            if (!ModelState.IsValid)
            {
               return View(moduleDto);
            }
            var teacherId = GetCurrentTeacherId(); // returns int (default 0 if not found);
            var course = await _context.Courses.FindAsync(id);
            if (course == null || course.TeacherId != teacherId)
            {
                return Forbid(); // prevent creating in another teacher’s course
            }
            var module = _mapper.Map<Module>(moduleDto);
            module.CourseId = course!.Id; // Set the CourseId from the course
            _context.Add(module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "User");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Modules.FirstOrDefaultAsync(i => i.Id == id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Title, Description, EnrollmentCount")] ModuleDTO moduleDto)
        {
            if (!ModelState.IsValid)
            {
               return View(moduleDto);
            }
            var module = await _context.Modules.FindAsync(id);
            if (module != null)
            {
                _mapper.Map(moduleDto, module); // Efficient mapping using AutoMapper
                _context.Update(module);
                await _context.SaveChangesAsync();
                return RedirectToAction("Manage", "Module");
            }
            
            return View(module);
        }
    }
}
