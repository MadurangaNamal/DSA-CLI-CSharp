using AutoMapper;
using practice.practiceFiles.Models;

namespace practice.practiceFiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<PersonDto, Person>()
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
             .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
    }
}
