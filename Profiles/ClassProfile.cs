using AutoMapper;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;

namespace SchoolAPI.Profiles
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<ClassModelIn, Class>()
                .ForMember(output => output.Name, options => options.MapFrom(input => input.Name))
                .ForMember(output => output.Capacity, options => options.MapFrom(input => input.Capacity))
                .ForMember(output => output.TeacherId, options => options.MapFrom(input => input.TeacherId))
                ;

        }
    }
}
