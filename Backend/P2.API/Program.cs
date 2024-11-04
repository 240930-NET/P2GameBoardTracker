using Microsoft.EntityFrameworkCore;
using P2.API.Data;
using P2.API.Model.DTO; 
using P2.API.Model; 
using P2.API.Controller;
using P2.API.Repository;
using P2.API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//adddbcontext
//addscoped
//addscoped
// Console.WriteLine(builder.Configuration.GetConnectionString("P2"));
builder.Services.AddDbContext<BacklogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("P1")));
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

builder.Services.AddControllers().AddNewtonsoftJson();;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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