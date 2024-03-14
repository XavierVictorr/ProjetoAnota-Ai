using AutoMapper;
using Domain.DTOS.User;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Interfaces.Services.User;
using Domain.Models;

namespace Service.Services;

public class UserServices : IUserService
{
    private IRepository<UserEntity> _repository;
    private readonly IMapper _mapper;

    public UserServices(IRepository<UserEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<UserDto> Get(Guid id)
    {
        var entity = await _repository.SelectAsync(id);
        return _mapper.Map<UserDto>(entity);
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        var listEntity = await _repository.SelectAsync();
        return _mapper.Map<IEnumerable<UserDto>> (listEntity);
    }

    public async Task<UserDtoCreateResult> Post(UserDtoCreate User)
    {
        // mapper de dto para model
        var model = _mapper.Map<UserModels>(User);
        
        //mapper de mode para entity
        var entity = _mapper.Map<UserEntity>(model);
        
        //chamada para repositorio / banco de dados
        var result = await _repository.InsentAsync(entity);
        
        // mapper de entity para dto
        return _mapper.Map<UserDtoCreateResult>(result);
    }

    public async Task<UserDtoUpdateResult> Put(UserDtoUpadate user)
    {
        var model = _mapper.Map<UserModels> (user);
        var entity = _mapper.Map<UserEntity> (model);
        var result = await _repository.UpdateAsync (entity);
        return _mapper.Map<UserDtoUpdateResult>(result);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}