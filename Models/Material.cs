using System.ComponentModel.DataAnnotations.Schema;

namespace LmsProject.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FileUploadPath { get; set; }
        public string ExternalLink { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public int ModuleId { get; set; } // Foreign key to Course
        public Module Module { get; set; } // Navigation property to Course

        public List<ModuleMaterial> ModuleMaterials { get; set; }
        public string FileExtension => string.IsNullOrEmpty(FileUploadPath)
        ? ""
        : Path.GetExtension(FileUploadPath);
    }
}