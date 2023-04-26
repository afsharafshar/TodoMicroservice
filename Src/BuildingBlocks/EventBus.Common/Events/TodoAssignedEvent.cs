namespace EventBus.Common.Events;

public class TodoAssignedEvent:BaseEvent
{
    public string? TodoId { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string UserCreated { get; set; } = string.Empty;
    public string? UserAssigned { get; set; }
    public DateTime Updated { get; set; }
}