namespace LmsProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int EnrollmentCount { get; set; } = 0; // Default to 0, can be updated as students enroll
        public List<StudentCourse>? StudentCourses { get; set; }
        public List<Module> Modules { get; set; } = new();

    }
}
