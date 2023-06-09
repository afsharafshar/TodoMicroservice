using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSOService.Api.Services;
using SSOService.Api.ViewModels;

namespace SSOService.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    

    private readonly ILogger<UserController> _logger;
    private readonly IIdentityService _identityService;

    public UserController(ILogger<UserController> logger,IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterViewModel userRegister)
    {
       bool result=await _identityService.RegisterUser(userRegister.Email, userRegister.UserName, userRegister.Password);
       return Ok(result);
    } 
    
    [HttpPost]
    public async Task<IActionResult> Login(UserRegisterViewModel userRegister)
    {
       string token=await _identityService.Login(userRegister.Email,userRegister.Password);
       return Ok(token);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ReNewToken()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        var userId2 = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        string token=await _identityService.GenerateToken(userId);
        return Ok(token);
    }
}
