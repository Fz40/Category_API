using AutoMapper;
using API.Dtos;
using API.Models;

namespace API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // * CreateMap<Source , Target>
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();

        }
    }
}