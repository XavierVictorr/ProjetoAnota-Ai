using AutoMapper;
using Data.Mapping;
using Domain.Entity;
using Domain.Models;

namespace CrossCutting.Mappings;

public class ModelToEntityProile : Profile
{
    public ModelToEntityProile()
    {
        CreateMap<UserEntity, UserModels>()
            .ReverseMap();
    }
}