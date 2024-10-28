using Microsoft.EntityFrameworkCore;
using P2.API.Data;
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

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IBacklogService, BacklogService>();

builder.Services.AddScoped<IBacklogRepository, BacklogRepository>();
builder.Services.AddControllers().AddNewtonsoftJson();;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();

