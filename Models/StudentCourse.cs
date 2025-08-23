namespace LmsProject.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        // You can also add extra columns if needed
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}
