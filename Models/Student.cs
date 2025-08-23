namespace LmsProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<StudentCourse>? StudentCourses { get; set; }
    }
}
