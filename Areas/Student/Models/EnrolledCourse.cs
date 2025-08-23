using LmsProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LmsProject.Areas.Student.Models
{
    public class EnrolledCourse : PageModel
    {
        public List<Course> Courses { get; set; }

        

    }
}
