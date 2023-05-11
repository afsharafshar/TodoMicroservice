using IdentityCommon;
using Serilog;
using Serilog.Common;
using Swagger.Common;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog(Serilogger.Configure);
builder.Services.AddIdentityCommon();
builder.Services.AddSwaggerCommon("NotificationService");

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();