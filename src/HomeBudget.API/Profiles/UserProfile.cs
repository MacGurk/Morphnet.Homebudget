using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.UserModels;

namespace HomeBudget.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserForCreationDto, User>();
        CreateMap<UserForUpdateDto, User>();
    }
}