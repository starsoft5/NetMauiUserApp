using Microsoft.EntityFrameworkCore;
using MyMauiApp.Data.Models;

public class AppDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<MyMauiApp.Data.Models.Task> Tasks{ get; set; }
    public DbSet<User> Users { get; set; }


    private readonly string _dbPath;

    public AppDbContext()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _dbPath = Path.Combine(folder, "XamarinDb.db");
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }
    }
}
