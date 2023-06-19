using AutoMapper;
using backend.Models;
using backend.Models.Dto;

namespace backend
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserCreateDto, User>().ReverseMap(); 
        }
    }
}
