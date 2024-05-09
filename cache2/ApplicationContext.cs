using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!; 

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) => Database.EnsureCreated();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // инициализация БД начальными данными
        modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Иван" },
                new User { Id = 2, Name = "Борис" },
                new User { Id = 3, Name = "Катя" }
        );
    }



}