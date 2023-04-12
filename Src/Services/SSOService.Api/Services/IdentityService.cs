using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityCommon;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSOService.Api.Entities;

namespace SSOService.Api.Services;

public interface IIdentityService
{
    Task<string> GenerateToken(string? userId);
    Task<string> Login(string email, string pass);
    Task<bool> RegisterUser(string email,string userName, string password);
}

public class IdentityService : IIdentityService
{

    private readonly JwtConfig _jwtConfig;
    private readonly UserManager<AppUser> _userManager;
    private readonly byte[] _secret;

    public IdentityService(
       IOptions<JwtConfig> jwtConfig,
        UserManager<AppUser> userManager)
    {
        this._jwtConfig = jwtConfig.Value;
        this._userManager = userManager;
        _secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
    }

    
    public async Task<bool> RegisterUser(string? email,string? username, string? password)
    {
        if (string.IsNullOrWhiteSpace(username)|| string.IsNullOrWhiteSpace(email)|| string.IsNullOrWhiteSpace(password))
        {
            return false;
        }
        
        var user = new AppUser { UserName = username ,Email=email};
        var usr = await _userManager.CreateAsync(user, password);
        //TODO send userErrors
        return usr.Succeeded;
       
    }

    public async Task<string> Login(string email, string pass)
    {
        var finduser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (finduser == null)
            return String.Empty;
        var result = await _userManager.CheckPasswordAsync(finduser, pass);

        if (result)
        {
            var token= await GenerateToken(finduser.Id.ToString());
            return token;
        }
        return string.Empty;
    }
    
    public async Task<string> GenerateToken(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return string.Empty;
        }
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return string.Empty;
       
        var roles = await _userManager.GetRolesAsync(user);

        if (user.UserName != null)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            
            var tokenSecurity = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.Now.AddDays(_jwtConfig.AccessTokenExpiry),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSecurity);

            return token;
        }

        return string.Empty;
    }

}