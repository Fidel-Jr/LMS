using Microsoft.AspNetCore.Identity;

namespace LmsProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int age { get; set; }
        public string? Address { get; set; }
    }
}
