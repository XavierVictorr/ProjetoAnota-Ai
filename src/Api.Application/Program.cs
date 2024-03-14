using AutoMapper;
using CrossCutting.DependencyInjection;
using CrossCutting.Mappings;
using Data.Context;
using Data.Implementations;
using Data.Repository;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Interfaces.Services.User;
using Domain.Repository;
using Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnotaAi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
    { 
        Name = "Authorization", 
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer", 
        BearerFormat = "JWT", 
        In = ParameterLocation.Header, 
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"", 
    }); 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
    { 
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                } 
            }, 
            new string[] {} 
        } 
    }); 
});



/*builder.Services.AddDbContext<MyContext>(
    options => options.UseMySql("server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=Teste@123") 
    );*/
//MySqlConfigurations 
var mySqlConnection = "server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=Teste@123";
builder.Services.AddDbContextPool<MyContext>(options =>
    options.UseMySql(mySqlConnection,
        ServerVersion.AutoDetect(mySqlConnection)));
//TokenConfigurations 
var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
        builder.Configuration.GetSection("TokenConfigurations"))
    .Configure(tokenConfigurations);

//InjeçãodeDependencia
    //builder.Services.AddTransient<IServiceCollection, ServiceCollection>();
    //service
    
    builder.Services.AddSingleton( new SigningConfigurations());
    builder.Services.AddTransient<IUserService, UserServices> ();
    builder.Services.AddTransient<IloginService, LoginServive>();
    builder.Services.AddSingleton(tokenConfigurations);
    
    //repository
    builder.Services.AddScoped(typeof(IRepository<UserEntity>),typeof(BaseRepository<UserEntity>));
    builder.Services.AddScoped<IUserRepository, UserImplementation>();

    //Authentications
    builder.Services.AddAuthentication(authOptions =>
    {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(bearerOptions =>
    {
        var paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = SigningConfigurations.Key;
        paramsValidation.ValidAudience = tokenConfigurations.Audience;
        paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
        paramsValidation.ValidateIssuerSigningKey = true;
        paramsValidation.ValidateLifetime = true;
        paramsValidation.ClockSkew = TimeSpan.Zero;
    });
    builder.Services.AddAuthorization(auth =>
    {
        auth.AddPolicy("Bearer", 
            new AuthorizationPolicyBuilder(new []
                    { JwtBearerDefaults.AuthenticationScheme }
                )
            .RequireAuthenticatedUser().Build());
    });
//MappesConfigurations 
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new DtoToModelProfile());
        cfg.AddProfile(new EntityToDtoProfile());
        cfg.AddProfile(new ModelToEntityProile());
    });
    IMapper mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper); 



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
