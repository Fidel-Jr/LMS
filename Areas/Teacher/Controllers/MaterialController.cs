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
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MaterialController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public IActionResult Index(int id)
        {
            var course = _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefault(c => c.Id == id);

            return View(course); // Pass the course data to the view
        }
        public IActionResult Create(int id)
        {
            var course = _context.Courses.Where(c => c.Id == id)
                .Select(c=>c.Title)
                .FirstOrDefault();
            ViewData["Title"] = course;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, [Bind("Title, Description")] ModuleDTO moduleDto)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.GetUserAsync(User);

                // Example: Get user ID
                var userId = _userManager.GetUserId(User);
                var teacherId = _context.Teachers
                                .Where(t => t.UserId == userId)
                                .Select(t => t.Id)
                                .FirstOrDefault(); // returns int (default 0 if not found);

                var module = new Module
                {
                    Title = moduleDto.Title,
                    Description = moduleDto.Description,
                    CourseId = id
                };
                _context.Add(module);
                await _context.SaveChangesAsync();  
                return RedirectToAction("Index", "User");
            }
            return View(moduleDto);
        }
    }
}
