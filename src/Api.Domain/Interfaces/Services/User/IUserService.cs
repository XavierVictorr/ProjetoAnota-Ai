using Domain.DTOS.User;
using Domain.Entity;

namespace Domain.Interfaces.Services.User;

public interface IUserService
{
    Task<UserDto> Get (Guid id); 
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDtoCreateResult> Post(UserDtoCreate User);
    Task<UserDtoUpdateResult> Put(UserDtoUpadate user);
    Task<bool> Delete (Guid id);
}