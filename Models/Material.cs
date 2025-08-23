namespace LmsProject.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow; // Default to current time
        public int ModuleId { get; set; } // Foreign key to Course
        public Module Module { get; set; } // Navigation property to Course
    }
}