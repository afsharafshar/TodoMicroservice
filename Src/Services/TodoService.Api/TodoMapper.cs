using Riok.Mapperly.Abstractions;
using TodoService.Api.Entities;
using TodoService.Api.ViewModels;

namespace TodoService.Api;

[Mapper]
public partial class TodoMapper
{
    public partial Todo TodoViewModelToTodo(TodoViewModel todoVm);
}