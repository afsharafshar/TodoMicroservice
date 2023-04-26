namespace EventBus.Common.Events;

public class BaseEvent
{
    public Guid Id { get; private set; }
        
    public DateTime CreationDate { get; private set; }

    public BaseEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public BaseEvent(Guid id,DateTime dateTime)
    {
        Id = id;
        CreationDate = dateTime;
    }
}