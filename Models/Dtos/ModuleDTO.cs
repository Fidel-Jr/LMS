using System.ComponentModel.DataAnnotations;

namespace LmsProject.Models.Dtos
{
    public class ModuleDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
