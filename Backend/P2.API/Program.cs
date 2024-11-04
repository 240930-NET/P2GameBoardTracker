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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//adddbcontext
//addscoped
//addscoped
// Console.WriteLine(builder.Configuration.GetConnectionString("P2"));
builder.Services.AddDbContext<BacklogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GameBoardTracker")));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBacklogService, BacklogService>();

builder.Services.AddScoped<IBacklogRepository, BacklogRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers().AddNewtonsoftJson(); ;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure session services 
// this sets up in memory session storage and configures session options 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	// setting the timeout for testing 
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	// make the session cookie essential 
	options.Cookie.IsEssential = true;
});


// configure the cookie based auth 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
	// set the login path for redirection when the auth is required 
	options.LoginPath = "/api/User/login";
	// Set the logout path
	options.LogoutPath = "/api/User/logout";
	// Set the access denied path for redirection when authorization fails
	options.AccessDeniedPath = "/api/User/AccessDenied";
	// Set sliding expiration to true to automatically renew the authentication ticket
	options.SlidingExpiration = true;
	// Set the expiration time for the authentication cookie
	options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeManager API v1"));
}
/*changes made 
	added a redirect 
	change routing to default to 
	/swagger on default 

*/
app.UseHttpsRedirection();
app.UseAuthorization();




// Redirect root URL to Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();