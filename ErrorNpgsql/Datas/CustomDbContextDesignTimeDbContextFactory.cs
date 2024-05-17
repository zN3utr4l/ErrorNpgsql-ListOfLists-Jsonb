using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ErrorNpgsql.Datas;

public class CustomDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomDbContext>
{
    public CustomDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<CustomDbContext> builder = new();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(connectionString);

        builder.EnableSensitiveDataLogging();
        builder.UseLazyLoadingProxies();
        builder.EnableDetailedErrors();
        builder.UseSnakeCaseNamingConvention();

        return new CustomDbContext(builder.Options);
    }
}