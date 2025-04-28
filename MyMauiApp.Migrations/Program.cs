using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(folderPath, "XamarinDb.db"); // Change this path as needed
        //options.UseSqlite($"Data Source={dbPath}");
        //if (File.Exists(dbPath))
        //{
         //   File.Delete(dbPath);
        //}

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}")); // Same as your real app

        var serviceProvider = services.BuildServiceProvider();

        // You don't need to run anything here for migrations
        Console.WriteLine("Migration project ready.");

    }
}
