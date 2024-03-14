using AutoMapper;
using Domain.DTOS.User;
using Domain.Models;

namespace CrossCutting.Mappings;

public class DtoToModelProfile : Profile
{
    public DtoToModelProfile()
    {
        CreateMap<UserModels, UserDto>()
            .ReverseMap();
        CreateMap<UserModels, UserDtoCreate>()
            .ReverseMap();
        CreateMap<UserModels, UserDtoUpadate>()
            .ReverseMap();
    }
}