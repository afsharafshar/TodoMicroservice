using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IdentityCommon;

public static class IdentityCommonExtension
{
    public static IServiceCollection AddIdentityCommon(this IServiceCollection services)
    {
        #region Authentication

        var configuration = new ConfigurationBuilder().AddJsonFile("JwtConfig.json").Build();
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
            .AddJwtBearer(options =>    
            {    
                options.TokenValidationParameters = new TokenValidationParameters    
                {    
                    ValidateIssuer = true,    
                    ValidateAudience = true,    
                    ValidateLifetime = true,    
                    ValidateIssuerSigningKey = true,    
                    ValidIssuer = configuration["JwtConfig:Issuer"],    
                    ValidAudience = configuration["JwtConfig:Issuer"],    
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:secret"]!))    
                };    
            }); 

        services.Configure<IdentityOptions>(ops =>
        {
            ops.Password.RequiredLength = 3;
            ops.Password.RequireNonAlphanumeric = false;
            ops.Password.RequireUppercase = false;
            ops.Password.RequireLowercase=false;
            ops.SignIn.RequireConfirmedEmail = false;
        });


        #endregion

        return services;
    }
}