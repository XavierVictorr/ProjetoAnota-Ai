using Data.Implementations;
using Data.Repository;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;

public class ConfigureRepository
{
    public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>));
        serviceCollection.AddScoped<IUserRepository, UserImplementation>();
        //serviceCollection.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>));
    }
}