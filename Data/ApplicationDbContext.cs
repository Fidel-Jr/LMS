using LmsProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LmsProject.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentCourse>().HasKey(ic => new
        {
            ic.StudentId,
            ic.CourseId
        });
        modelBuilder.Entity<ModuleMaterial>().HasKey(ic => new
        {
            ic.ModuleId,
            ic.MaterialId
        });
        modelBuilder.Entity<StudentCourse>()
           .HasOne(sc => sc.Student)
           .WithMany(s => s.StudentCourses)
           .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);
        modelBuilder.Entity<ModuleMaterial>()
           .HasOne(m => m.Module)
           .WithMany(mm => mm.ModuleMaterials)
           .HasForeignKey(sc => sc.ModuleId);
        modelBuilder.Entity<ModuleMaterial>()
           .HasOne(m => m.Material)
           .WithMany(mm => mm.ModuleMaterials)
           .HasForeignKey(m => m.MaterialId);
        // Many to Many Foreign Key Creation
        modelBuilder.Entity<Teacher>().HasData(
            new Teacher { Id = 1, UserId = "75cf164f-e9e0-4772-9b06-f87a31405c57", Description = "English Teacher" },
            new Teacher { Id = 2, UserId = "3c3fa81f-aa22-4177-a033-f8731a6de578", Description = "Physics Teacher" },
            new Teacher { Id = 3, UserId = "7500d3ec-929a-48dd-994f-73526a042e62", Description = "IT Staff" }
        );
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, UserId = "180aae21-8b04-4495-9927-67c83639fcc9" },
            new Student { Id = 2, UserId = "a928b094-3f85-45bc-9bb8-96aa99f92f88" },
            new Student { Id = 3, UserId = "180aae21-8b04-4495-9927-67c83639fcc9" }
        );

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<EnrollmentRequest> EnrollmentRequests { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<ModuleMaterial> ModuleMaterials { get; set; }
}
