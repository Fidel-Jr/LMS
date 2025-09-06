using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmsProject.Models.Dtos
{
    public class MaterialDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string? FileUploadPath { get; set; }
        public string? ExternalLink { get; set; }
        //[NotMapped]  // Prevents EF from mapping this property
        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
