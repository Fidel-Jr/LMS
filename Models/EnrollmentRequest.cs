namespace LmsProject.Models
{
    public class EnrollmentRequest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }    
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Possible values: Pending, Approved, Rejected

    }
}
