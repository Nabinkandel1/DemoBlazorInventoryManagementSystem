using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Build configuration  
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Ensure Microsoft.Extensions.Hosting is referenced  
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        // Configure DbContext  
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(connectionString, options =>
        {
            options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        });

        return new ApplicationDbContext(builder.Options);
    }
}
