using Microsoft.AspNetCore.Mvc;
using TodoService.Api.Services;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
   
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoService _todoService;

    public TodoController(ILogger<TodoController> logger,ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var todos =await _todoService.GetTodoAsync();
        return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TodoViewModel todoViewModel)
    {
       await _todoService.CreateTodo(todoViewModel);
       return Ok();
    }
}