using EventBus.Common.Events;
using MassTransit;
using TodoService.Api.Entities;
using TodoService.Api.Repositories;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Services;

public class TodoService:ITodoService
{
    private  TodoMapper TodoMapper { get; }
    private readonly ITodoRepository _todoRepository;
    private readonly IPublishEndpoint  _publisher;

    public TodoService(ITodoRepository todoRepository,TodoMapper todoMapper,IPublishEndpoint  publisher)
    {
        TodoMapper = todoMapper;
        _todoRepository = todoRepository;
        _publisher = publisher;
    }


    public async Task CreateTodo(TodoCreateViewModel todoCreateVm)
    {
        var todo =TodoMapper.TodoViewModelToTodo(todoCreateVm);
        todo.Created=DateTime.Now;
        todo.Updated=DateTime.Now;
        todo.UserAssigned ??= todo.UserCreated;
        
        await _todoRepository.CreateAsync(todo);
        
        if (todo.UserAssigned != todo.UserCreated)
        {
            await  _publisher.Publish<TodoAssignedEvent>(new TodoAssignedEvent()
            {
                TodoId = todo.Id,
                UserAssigned = todo.UserAssigned,
                UserCreated = todo.UserCreated,
                Updated = todo.Updated,
                Title = todo.Title
            });
        }
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