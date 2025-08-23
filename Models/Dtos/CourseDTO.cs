using System.ComponentModel.DataAnnotations;

namespace LmsProject.Models.Dtos
{
    public class CourseDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int EnrollmentCount { get; set; } = 0; // Default to 0, can be updated as students enroll
    }
}
