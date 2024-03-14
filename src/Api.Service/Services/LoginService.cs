using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Domain.DTOS;
using Domain.Entity;
using Domain.Interfaces.Services.User;
using Domain.Repository;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Service.Services;

public class LoginServive : IloginService
{
    private IUserRepository _repository;
    private SigningConfigurations _signingConfigurations;
    private TokenConfiguration _tokenConfiguration;
    private IConfiguration _configuration { get;}
    
    public LoginServive(IUserRepository repository,
                        SigningConfigurations signingConfigurations,
                        TokenConfiguration tokenConfiguration,
                        IConfiguration configuration)
    {
        _repository = repository;
        _signingConfigurations = signingConfigurations;
        _tokenConfiguration = tokenConfiguration;
        _configuration = configuration;
    }
    public async Task<object> FindByLogin(LoginDto user)
    {
        var baseUser = new UserEntity();
        if (user != null && !string.IsNullOrWhiteSpace(user.Email))
        {
            baseUser = await _repository.FindByLogin(user.Email);
            if (baseUser == null)
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao Autenticar"
                };
            }
            else
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(baseUser.Email),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    }
                );
                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, baseUser);
            }  
        }
        else
        {
            return new
            {
                authenticated = false,
                message = "Falha ao Autenticar"
            };
        }
    }
    
    
    private string CreateToken  (ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
    {
        try
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,                
            });
                 
            var token = handler.WriteToken(securityToken);
            return token; 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
                   
    }
    
    private object SuccessObject(DateTime createDate, DateTime expiationDate, string token, UserEntity user)
    {
        return new 
        {
            authenticated = true,
            created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
            expiration = expiationDate.ToString("yyyy-MM-dd HH:mm:ss"),
            acessToken = token,
            userName = user.Email,
            name = user.Name,
            message = "Usuário logado com sucesso"
        };  
    }
}   