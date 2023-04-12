using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSOService.Api.Entities;

namespace SSOService.Api.DataAccess;

public class AppDbContext:IdentityDbContext<AppUser,AppRole,int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
}