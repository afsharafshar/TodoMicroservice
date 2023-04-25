using TodoService.Api.Entities;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Repositories;

public interface ITodoRepository
{
    Task<List<Todo>> GetAsync();
    Task<List<Todo>> GetAsync(TodoFilterViewModel filterViewModel);
    Task<Todo?> GetAsync(string id);
    Task CreateAsync(Todo newTodo);
    Task UpdateAsync(string id, Todo updatedTodo);
    Task RemoveAsync(string id);
    Task<List<Todo>> SearchTodo(string search);
    Task<long> CountUnCompleted(string userId);
}