namespace TodoService.Api;

public class TodoConfig
{
    public string ConnectionString { get; set; }=String.Empty;
    public string DatabaseName { get; set; }=String.Empty;
    public string TodosCollectionName{ get; set; }=String.Empty;
}
