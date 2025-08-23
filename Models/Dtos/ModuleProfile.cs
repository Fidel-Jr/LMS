using AutoMapper;

namespace LmsProject.Models.Dtos
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<ModuleDTO, Module>();
            CreateMap<Module, ModuleDTO>();
        }
    }
}
