using TodoService.Api.Entities;
using TodoService.Api.Repositories;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Services;

public class TodoService:ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }


    public async Task CreateTodo(TodoViewModel todoVm)
    {
        var todo = new TodoMapper().TodoViewModelToTodo(todoVm);
        todo.Created=DateTime.Now;
        todo.Updated=DateTime.Now;
        todo.UserAssigned ??= todo.UserCreated;

        if (todo.UserAssigned != todo.UserCreated)
        {
            // send notification 
        }

        await _todoRepository.CreateAsync(todo);
    }

    public Task<List<Todo>> GetTodoAsync()
    {
        //TODO Add paging
        return _todoRepository.GetAsync();
    }

    public Task<Todo?> GetTodoById(string id)
    {
        return _todoRepository.GetAsync(id);
    }

    public Task DeleteTodo(string id)
    {
        return _todoRepository.RemoveAsync(id);
    }
    
    

}