using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;

public abstract class BaseTest
{
    public BaseTest()
    {
        
    }
}

public class DbTest : IDisposable
{
    private string dataBaseName = $"dbApiTest_ {Guid.NewGuid().ToString().Replace("-", string.Empty)}";
    
    public ServiceProvider ServiceProvider { get; private set; }

    public DbTest()
    {
        var serviceCollection = new ServiceCollection ();
        var mySqlConnection = "server=localhost;Port=3306;Database=dbApiTest;Uid=root;Pwd=Teste@123";
        serviceCollection.AddDbContextPool<MyContext>(options =>
            options.UseMySql(mySqlConnection,
                ServerVersion.AutoDetect(mySqlConnection)));
        
        ServiceProvider = serviceCollection.BuildServiceProvider();
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context.Database.EnsureCreated();
        }
    }

    public void Dispose()
    {
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context.Database.EnsureDeleted();
        }
    }
}