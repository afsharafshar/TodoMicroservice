using System.ComponentModel.DataAnnotations;

namespace TodoService.Api.ViewModels;

public class TodoViewModel
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Title must be Inserted")]
    public string? Title { get; set; }
    
    [Required(ErrorMessage = "Description must be Inserted")]
    public string? Description { get; set; } 
    
    public bool Completed { get; set; }
    
    public string? UserAssigned { get; set; }
}