using TodoService.Api.Entities;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Services;

public interface ITodoService
{
    Task CreateTodo(TodoViewModel todoVm);
    Task<List<Todo>> GetTodoAsync();
    Task<Todo?> GetTodoById(string id);
    Task DeleteTodo(string id);
}