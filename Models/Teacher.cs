namespace LmsProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Description { get; set; } 
    }
}
