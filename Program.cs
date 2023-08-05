using System.Net.Mime;
using System.Text.Json;
using Catalog.Auth;
using Catalog.Helpers;
using Catalog.Repositories;
using Catalog.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mongoDbSettings = builder.Configuration.GetSection("CatalogDatabase");
var connectionString = mongoDbSettings.Get<MongoDbSettings>().ConnectionString;

builder.Services.Configure<MongoDbSettings>(mongoDbSettings);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();
builder.Services.AddSingleton<IJwtUtils, JwtUtils>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();

builder.Services.AddCors();
builder.Services.AddControllers(options =>
{
  options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddMongoDb(connectionString, name: "mongodb", timeout: TimeSpan.FromSeconds(3), tags: new[] { "ready" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
  Predicate = (check) => check.Tags.Contains("ready"),
  ResponseWriter = async (context, report) =>
  {
    var result = JsonSerializer.Serialize(
      new
      {
        status = report.Status.ToString(),
        checks = report.Entries.Select(entry => new
        {
          name = entry.Key,
          status = entry.Value.Status.ToString(),
          exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
          duration = entry.Value.Duration.ToString()
        })
      }
    );

    context.Response.ContentType = MediaTypeNames.Application.Json;
    await context.Response.WriteAsync(result);
  }
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
  Predicate = (_) => false
});

app.Run();

// https://youtu.be/ZXdFisA_hOY 3:01:00