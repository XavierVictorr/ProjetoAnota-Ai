using Domain.DTOS;
using Domain.Entity;

namespace Domain.Interfaces.Services.User;

public interface IloginService
{
    Task<object> FindByLogin(LoginDto user);
}