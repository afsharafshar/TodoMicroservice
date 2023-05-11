using IdentityCommon;
using MassTransit;
using Serilog;
using Serilog.Common;
using Swagger.Common;
using TodoService.Api;
using TodoService.Api.Repositories;
using TodoService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Serilogger.Configure);
builder.Services.AddIdentityCommon();
builder.Services.AddControllers();
builder.Services.AddSingleton<TodoMapper>();
builder.Services.AddSingleton<ITodoRepository,TodoRepository>();
builder.Services.AddScoped<ITodoService,TodoService.Api.Services.TodoService>();
builder.Services.AddSwaggerCommon("TodoService");
builder.Services.AddMassTransit(config =>
{
    // config.AddConsumer<BasketCheckoutConsumer>();
    
        config.UsingRabbitMq((ctx, cfg) => {
            cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
            
            // cfg.UseHealthCheck(ctx);
            
            // cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
            //     c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
            // });
        });
   
});


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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();