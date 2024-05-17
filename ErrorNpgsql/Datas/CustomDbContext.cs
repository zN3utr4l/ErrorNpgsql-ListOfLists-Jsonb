using ErrorNpgsql.ExtensionMethods;
using ErrorNpgsql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ErrorNpgsql.Datas;

public class CustomDbContext(DbContextOptions<CustomDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MyEntityDbConfiguration());

        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.ToSnakeCase());
        }
    }

    public DbSet<MyEntity> MyEntity { get; set; }
}