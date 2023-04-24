using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoService.Api.Entities;

public class Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Completed { get; set; }
    public string UserCreated { get; set; } = string.Empty;
    public string? UserAssigned { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}