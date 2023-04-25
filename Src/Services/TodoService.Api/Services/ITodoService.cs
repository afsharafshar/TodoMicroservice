using TodoService.Api.Entities;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Services;

public interface ITodoService
{
    Task CreateTodo(TodoCreateViewModel todoCreateVm);
    Task<List<Todo>> GetTodoAsync(TodoFilterViewModel filterViewModel);
    Task<Todo?> GetTodoById(string id);
    Task DeleteTodo(string id);
}