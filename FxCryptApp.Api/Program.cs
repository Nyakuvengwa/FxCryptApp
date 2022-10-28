using System.Reflection;
using FxCryptApp.Api.Middleware;
using FxCryptApp.Common;
using FxCryptApp.Data;
using FxCryptApp.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FxCryptAppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(FxCryptAppDbContext)), b => b.MigrationsAssembly("FxCryptApp.Data"))            
			.LogTo(Console.WriteLine, LogLevel.Information)
			.EnableSensitiveDataLogging()
			.EnableDetailedErrors();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterFxCryptAppCommon();
builder.Services.RegisterFxCryptAppServices();
builder.Services.RegisterFxCryptAppRepositories();
builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<FxCryptAppDbContext>();
	if (context.Database.GetPendingMigrations().Any())
	{
		context.Database.Migrate();
	}

}


app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
