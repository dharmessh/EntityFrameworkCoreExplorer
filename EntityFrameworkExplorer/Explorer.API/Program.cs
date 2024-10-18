using Explorer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setting up DBContext
var sLiteDbName = builder.Configuration.GetConnectionString("SqlLiteDb");
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Combine(path, sLiteDbName);
var connectionString = $"Data Source={dbPath}";

// Ctor with Options comes into play here in DbContext Class.
// This accomplish the task of OnConfiguring Method as well 
builder.Services.AddDbContext<FootballDbContext>(options =>
{
    options.UseSqlite(connectionString, sliteOptions =>
    {
        sliteOptions.CommandTimeout(30);
    })
               //.UseLazyLoadingProxies()
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)  // Globally setting as No Tracking 
               .LogTo(Console.WriteLine, LogLevel.Information);

    if (!builder.Environment.IsProduction())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors(); 
    }
               
});

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

app.Run();
