using IdentityCommon;
using TodoService.Api;
using TodoService.Api.Repositories;
using TodoService.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentityCommon();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITodoRepository,TodoRepository>();
builder.Services.AddSingleton<ITodoService,TodoService.Api.Services.TodoService>();

builder.Services.Configure<TodoConfig>(
    builder.Configuration.GetSection("TodoConfig"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();