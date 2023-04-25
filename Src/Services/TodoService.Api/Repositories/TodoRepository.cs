using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoService.Api.Entities;
using TodoService.Api.ViewModels;

namespace TodoService.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IMongoCollection<Todo> _todoCollection;
    
    public TodoRepository(IOptions<TodoConfig> todoOptions )
    {
        var todoConfig = todoOptions.Value;
        var mongoClient = new MongoClient(todoConfig.ConnectionString);
        var database = mongoClient.GetDatabase(todoConfig.DatabaseName);
        _todoCollection = database.GetCollection<Todo>(todoConfig.TodosCollectionName);
    }
    
    public async Task<List<Todo>> GetAsync() =>
        await _todoCollection.Find(_ => true).ToListAsync();

    public Task<List<Todo>> GetAsync(TodoFilterViewModel filterViewModel)
    {
        var filterDefinitions = new List<FilterDefinition<Todo>>();
        if (string.IsNullOrWhiteSpace(filterViewModel.UserId))
        {
            throw new Exception("User is not valid");
        }
        filterDefinitions.Add( Builders<Todo>.Filter.Where(x => x.UserAssigned==filterViewModel.UserId));
        
        if (!string.IsNullOrWhiteSpace(filterViewModel.Title))
        {
            filterDefinitions.Add( Builders<Todo>.Filter.Where(x => x.Title.Contains(filterViewModel.Title!)));
         
        }
        
        //todo Add other filters
        
        var filter = Builders<Todo>.Filter.Or(
            filterDefinitions
        );
       
        return  _todoCollection.Find(filter).ToListAsync();
    }

    public async Task<Todo?> GetAsync(string id) =>
        await _todoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Todo newTodo) =>
        await _todoCollection.InsertOneAsync(newTodo);

    public async Task UpdateAsync(string id, Todo updatedTodo) =>
        await _todoCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);

    public async Task RemoveAsync(string id) =>
        await _todoCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<List<Todo>> SearchTodo(string search)
    {
        var filter = Builders<Todo>.Filter.Or(
            Builders<Todo>.Filter.Where(x => x.Title.Contains(search)),
            Builders<Todo>.Filter.Where(x => x.Description.Contains(search))
        );
        // todo user access
        return await _todoCollection.Find(filter).ToListAsync();
    }

    public async Task<long> CountUnCompleted(string userId)
    {
        var filter = Builders<Todo>.Filter.Or(
            Builders<Todo>.Filter.Where(x => x.UserAssigned == userId),
            Builders<Todo>.Filter.Where(x=>x.Completed==false));
        return await _todoCollection.Find(filter).CountDocumentsAsync();
    }
}