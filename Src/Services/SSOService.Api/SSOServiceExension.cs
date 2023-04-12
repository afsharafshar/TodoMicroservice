using IdentityCommon;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSOService.Api.DataAccess;
using SSOService.Api.Entities;
using SSOService.Api.Services;

namespace SSOService.Api;

public static class SSOServiceExension
{
    public static IServiceCollection AddSSOService(this IServiceCollection services)
    {
    
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<AppDbContext>(opts => {
            //opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            opts.UseSqlite("Data Source=SSOService.db");
        });

        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        
        services.AddIdentityCommon();
        return services;
    }
}