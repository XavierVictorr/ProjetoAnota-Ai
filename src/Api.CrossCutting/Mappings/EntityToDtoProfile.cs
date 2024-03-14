using AutoMapper;
using Data.Mapping;
using Domain.DTOS.User;
using Domain.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CrossCutting.Mappings;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<UserDto, UserEntity>()
            .ReverseMap();
        CreateMap<UserDtoCreateResult, UserEntity>()
            .ReverseMap();
        CreateMap<UserDtoUpdateResult, UserEntity>()
            .ReverseMap();
    }
}