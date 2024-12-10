using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Data;
using MinimalApiDemo.Models;
using MinimalApiDemo.AppDataContext;
using MinimalApiDemo.Middleware;
using MinimalApiDemo.Models;


 // program.cs
var builder = WebApplication.CreateBuilder(args);


// Configurer le service DbContext avec une base In-Memory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// ajout des service aux container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddSingleton<TodoDbContext>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddLogging();

var app = builder.Build();

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider;
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/elements", async (AppDbContext db) =>
    await db.Elements.ToListAsync());

app.MapPost("/elements", async (AppDbContext db, Element element) =>
{
    db.Elements.Add(element);
    await db.SaveChangesAsync();
    return Results.Created($"/elements/{element.Id}", element);
});

app.MapGet("/", () => "Hello World!");

app.Run();
