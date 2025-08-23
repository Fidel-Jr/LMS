using AutoMapper;

namespace LmsProject.Models.Dtos
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();
        }
    }
}
