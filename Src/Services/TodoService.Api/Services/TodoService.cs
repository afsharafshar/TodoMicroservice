using TodoService.Api.Entities;
using TodoService.Api.Repositories;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Services;

public class TodoService:ITodoService
{
    private  TodoMapper TodoMapper { get; }
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository,TodoMapper todoMapper)
    {
        TodoMapper = todoMapper;
        _todoRepository = todoRepository;
    }


    public async Task CreateTodo(TodoCreateViewModel todoCreateVm)
    {
        var todo =TodoMapper.TodoViewModelToTodo(todoCreateVm);
        todo.Created=DateTime.Now;
        todo.Updated=DateTime.Now;
        todo.UserAssigned ??= todo.UserCreated;

        if (todo.UserAssigned != todo.UserCreated)
        {
            // send notification 
        }

        await _todoRepository.CreateAsync(todo);
    }

    public Task<List<Todo>> GetTodoAsync(TodoFilterViewModel filterViewModel)
    {
        //TODO Add paging
        return _todoRepository.GetAsync(filterViewModel);
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