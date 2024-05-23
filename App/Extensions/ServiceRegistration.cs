using App.Extensions.Jwt;
using App.IServices;
using App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions;

public class ServiceRegistration
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IJwtService, JwtService>();
    }
}