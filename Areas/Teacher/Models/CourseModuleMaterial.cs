using LmsProject.Models;

namespace LmsProject.Areas.Teacher.Models
{
    public class CourseModuleMaterial
    {
        public Course Course { get; set; }
        public List<Module> Modules { get; set; }
        public List<Material> Materials { get; set; }
        public string EnrollmentStatus { get; set; } = "Not Enrolled"; // Default to false, can be updated based on enrollment status
        public List<EnrollmentRequest> EnrollmentRequest { get; set; }
    }
}
