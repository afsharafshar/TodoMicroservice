using System.Security.Claims;
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

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("IdentitySetting.json",false,true)
            .Build();
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
        
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })    
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
            ops.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
        });

        services.AddAuthorization();

        #endregion

        return services;
    }
}