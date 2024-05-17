using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ErrorNpgsqlJsonb;

internal class Program
{
    private static void Main(string[] args)
    {
        ServiceCollection services = new();

        services.AddDbContext<MyDbContext>(x =>
        {
            x.EnableSensitiveDataLogging();
            x.UseLazyLoadingProxies();
            x.EnableDetailedErrors();
            x.UseSnakeCaseNamingConvention();
            x.UseNpgsql("Host=127.0.0.1:5432;Database=test;Username=postgres;Password=Test123!;Include Error Detail=true;Maximum Pool Size=500;Pooling=true;");
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        using MyDbContext? dbContext = serviceProvider.GetService<MyDbContext>();

        dbContext.Add(new MyEntity()
        {
            JsonbFields = [
                [new() { Key = "key1", Value = "value1"}, new() { Key = "key2", Value = "value2"}],
                [new() { Key = "key1", Value = "value1"}, new() { Key = "key2", Value = "value2"}]
            ]
        });

        dbContext.SaveChanges();

        foreach (MyEntity entity in dbContext.MyEntity)
        {
            Console.WriteLine(entity.ToString()); // Castle.Proxies.List`1Proxy ??? System.Collections.Generic.List`1[ErrorNpgsqlJsonb.JsonbField] Ok!!

            Console.WriteLine(JsonSerializer.Serialize(entity, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}

#region entities

public class MyEntity
{
    public int Id { get; set; }

    public List<List<JsonbField>>? JsonbFields { get; set; }
}

public class JsonbField
{
    public required string Key { get; set; }

    public required string Value { get; set; }
}

#endregion

#region dbcontext

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    public DbSet<MyEntity> MyEntity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MyEntity>(x => x.OwnsMany(x => x.JsonbFields, r => { r.ToJson(); }));

        Regex camelCaseRegex = new(@"([a-z0-9])([A-Z])", RegexOptions.None, TimeSpan.FromSeconds(5));

        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(camelCaseRegex.Replace(entity.GetTableName(), "$1_$2").ToLower(CultureInfo.InvariantCulture));
        }
    }
}

#endregion