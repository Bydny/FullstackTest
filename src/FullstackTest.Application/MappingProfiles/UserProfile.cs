using AutoMapper;
using FullstackTest.Application.DTOs;
using FullstackTest.Domain;

namespace FullstackTest.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
