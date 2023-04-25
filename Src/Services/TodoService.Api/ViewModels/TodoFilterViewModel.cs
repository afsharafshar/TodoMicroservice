namespace TodoService.Api.ViewModels;

public class TodoFilterViewModel
{
    public string? Title { get; set; }
    
    public string? Description { get; set; } 
    
    public bool? Completed { get; set; }

    public string UserId { get; set; } = string.Empty;
    public string? UserCreated { get; set; }

    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 15;
}
