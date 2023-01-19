using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.User;

namespace HomeBudget.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserForCreationDto, User>();
        CreateMap<UserForUpdateDto, User>();
    }
}