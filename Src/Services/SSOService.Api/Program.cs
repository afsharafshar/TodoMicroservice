using SSOService.Api;
using SSOService.Api.DataAccess;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSSOService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   using (var scope= app.Services.CreateScope())
   {
       var dbContext= scope.ServiceProvider.GetService<AppDbContext>();
       dbContext?.Database.EnsureCreated();

   }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();