using Domain.Entity;
using Domain.Interfaces;

namespace Domain.Repository;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity> FindByLogin (string email);
}