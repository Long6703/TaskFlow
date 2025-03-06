using Application.DTOs;
using AutoMapper;
using Domain.Entities;


namespace Application.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserCreateDTO>().ReverseMap();
        }
    }
}
