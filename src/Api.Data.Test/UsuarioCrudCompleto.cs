using Data.Context;
using Data.Implementations;
using Domain.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;

public class UsuarioCrudCompleto : BaseTest,IClassFixture<DbTest>
{
    private ServiceProvider _serviceProvide;	
    
    public UsuarioCrudCompleto(DbTest dbTeste)
    {
        _serviceProvide = dbTeste.ServiceProvider;
    }
    [Fact(DisplayName = "CRUD de Usuário")]
    [Trait("CRUD", "UserEntity")]
    
    public async Task E_Possivel_Realizar_CRUD_Usuario()
    {
        using (var context = _serviceProvide.GetService<MyContext>())
        {
            UserImplementation _repositorio = new UserImplementation(context);
            UserEntity _entity = new UserEntity
            {
                Email = "teste@mail.com",
                Name = "teste"
            };
							
            var _registroCriado = await _repositorio.InsentAsync(_entity);
                Assert.NotNull(_registroCriado);
            Assert.Equal("teste@mail.com", _registroCriado.Email);
            Assert.Equal("teste", _registroCriado.Name);    
            Assert.False(_registroCriado.id == Guid.Empty);
        }
    }
}