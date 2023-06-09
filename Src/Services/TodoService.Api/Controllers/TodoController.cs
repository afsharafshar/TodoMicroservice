using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoService.Api.Services;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TodoController : ControllerBase
{

    private readonly ILogger<TodoController> _logger;
    private readonly ITodoService _todoService;

    public TodoController(ILogger<TodoController> logger,ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }
    
    //TODO Remove get user id and add interface for ihttpaccessor to getuser
  
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]TodoFilterViewModel filterViewModel)
    {
        filterViewModel.UserId=HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var todos =await _todoService.GetTodoAsync(filterViewModel);
        return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TodoCreateViewModel todoCreateViewModel)
    {
        todoCreateViewModel.UserCreated=HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
       await _todoService.CreateTodo(todoCreateViewModel);
       return Ok();
    }
}