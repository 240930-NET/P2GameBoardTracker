using Microsoft.EntityFrameworkCore;
using P2.API.Data;
using P2.API.Model.DTO;
using P2.API.Model;
using P2.API.Controller;
using P2.API.Repository;
using P2.API.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var connectionString = builder.Configuration.GetConnectionString("GameBoardTracker");
Console.WriteLine($"Connection string: {connectionString}"); // Add this line for debugging

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'GameBoardTracker' not found.");
}

builder.Services.AddDbContext<BacklogContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddHttpClient<IGDBService>(client =>
{
    client.BaseAddress = new Uri("https://api.igdb.com/v4/");
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBacklogService, BacklogService>();
builder.Services.AddScoped<IBacklogRepository, BacklogRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/User/login";
        options.LogoutPath = "/api/User/logout";
        options.AccessDeniedPath = "/api/User/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeManager API v1"));
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();