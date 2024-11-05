namespace P2.API.Data;
using Microsoft.EntityFrameworkCore;
using P2.API.Model;
public partial class BacklogContext : DbContext
{
    public BacklogContext(){}
    public BacklogContext(DbContextOptions<BacklogContext> options) : base(options){}

    public virtual DbSet<User> Users {get;set;} 
    public virtual DbSet<Game> Games {get;set;} 
    public virtual DbSet<Backlog> Backlogs {get;set;} 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
            {
             e.Property(e => e.UserId)
             .ValueGeneratedOnAdd();
            }
        );
        modelBuilder.Entity<Game>(e =>
            {
             e.Property(e => e.GameId)
                .ValueGeneratedNever();
            //  .ValueGeneratedOnAdd();
            }
        );
        modelBuilder.Entity<User>().HasData(
            new User {UserId = 1, UserName = "Alfredo", Password = "Password",LastLoginDate = DateTime.UtcNow }
        );
        modelBuilder.Entity<Game>().HasData(
            new Game {GameId = 1, Name = "Sample Fake game"},
            new Game {GameId = 2, Name = "Sample Fake Game 2", Description = "Investigating a letter from his late wife, James returns to where they made so many memories - Silent Hill."}
        );
        modelBuilder.Entity<Backlog>().HasData(
            new Backlog {UserId = 1, GameId = 1, Completed = false}
        );

        
    }
}


