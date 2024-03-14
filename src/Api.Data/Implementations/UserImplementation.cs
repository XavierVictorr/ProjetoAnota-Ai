using Data.Context;
using Data.Repository;
using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementations;

    public class UserImplementation : BaseRepository<UserEntity>,IUserRepository
    {
        private DbSet<UserEntity> _dataSet;
    
    public UserImplementation(MyContext context) : base(context)
    {
        _dataset = context.Set<UserEntity>();
    }

    public async Task<UserEntity> FindByLogin(string email)
    {
        return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }
}