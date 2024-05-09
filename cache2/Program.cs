
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=cacheDB.db"));

builder.Services.AddTransient<UserService>();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapGet("/user/{id}", async (int id, UserService userService) =>
{
    User? user = await userService.GetUser(id);
    if (user != null) return $"User {user.Name}  Id={user.Id}";
    return "User not found";
});

app.MapGet("/", () => "Hello World!");

app.Run();
