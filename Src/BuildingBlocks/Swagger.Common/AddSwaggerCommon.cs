using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Swagger.Common;

public static class AddSwaggerCommonExtension
{
    public static void AddSwaggerCommon(this IServiceCollection service,string title="My App")
    {
        service.AddEndpointsApiExplorer();
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
    
}