using App.IServices;
using App.Jwt;
using App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration).Assembly);
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IFileNoteService, FileNoteService>();
    }
}