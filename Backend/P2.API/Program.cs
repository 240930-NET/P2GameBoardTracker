using Microsoft.EntityFrameworkCore;
using P2.API.Data;
using P2.API.Model.DTO;
using P2.API.Model;
using P2.API.Controller;
using P2.API.Repository;
using P2.API.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BacklogContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameBoardTracker")));
builder.Services.AddHttpClient<IGDBService>(client =>
{
    client.BaseAddress = new Uri("https://api.igdb.com/v4/");
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBacklogService, BacklogService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBacklogRepository, BacklogRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:5173") // Adjust this to your React app's URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Configure cookie authentication
builder.Services.AddAuthentication("CustomCookieAuth")
    .AddCookie("CustomCookieAuth", options =>
    {
        options.Cookie.Name = "YourAppAuthCookie";
        options.LoginPath = "/api/User/login";
        options.LogoutPath = "/api/User/logout";
        options.AccessDeniedPath = "/api/User/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "P2GameBoardTracker API v1"));
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

// Use session
app.UseSession();

// Redirect root URL to Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();