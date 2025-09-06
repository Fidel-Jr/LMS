using LmsProject.Data;
using LmsProject.Models;
using LmsProject.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace LmsProject.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        public MaterialController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
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
        public async Task<IActionResult> Create(int id, MaterialDto materialDto)
        {
            if (!ModelState.IsValid)
            {
                return View(materialDto);

            }
            if (materialDto.Files != null && materialDto.Files.Count > 0)
            {
                var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadFolder);
                foreach (var file in materialDto.Files)
                {
                    var ext = Path.GetExtension(file.FileName).ToLower();
                    var allowedExt = new[]
                    {
                        ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
                        ".txt", ".rtf", ".odt", ".ods", ".odp",
                        ".jpg", ".jpeg", ".png", ".gif", ".bmp",
                        ".mp4", ".mov", ".avi", ".mkv", ".wmv", ".flv"
                    };

                    var uniqueFileName = Guid.NewGuid().ToString() + ext;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the uploaded file path
                    materialDto.FileUploadPath = "/uploads/" + uniqueFileName;

                    
                }

            }
            var material = new Material
            {
                Title = materialDto.Title,
                Content = materialDto.Content,
                FileUploadPath = materialDto.FileUploadPath,
                ExternalLink = materialDto.ExternalLink,
                ModuleId = id
            };

            _context.Materials.Add(material);

            await _context.SaveChangesAsync();
            var moduleMaterial = new ModuleMaterial
            {
                ModuleId = id,
                MaterialId = material.Id
            };
            _context.ModuleMaterials.Add(moduleMaterial);
            await _context.SaveChangesAsync();

            return View(materialDto);
        }
    }
}
