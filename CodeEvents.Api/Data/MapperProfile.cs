using AutoMapper;
using CodeEvents.Api.Core;
using CodeEvents.Api.Core.DTOs;

namespace CodeEvents.Api.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<CodeEvent, CodeEventDto>().ReverseMap();
            CreateMap<Lecture, LectureDto>().ReverseMap();
        }
    }
}
