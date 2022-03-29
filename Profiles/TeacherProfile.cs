using AutoMapper;
using SchoolAPI.Models.EntityModels;
using SchoolAPI.Models.InputModels;

namespace SchoolAPI.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherModelIn, Teacher>()
                .ForMember(output => output.Name, options => options.MapFrom(input => input.Name))
                .ForMember(output => output.Email, options => options.MapFrom(input => input.Email))
                ;

        }
    }
}
