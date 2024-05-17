using ErrorNpgsql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErrorNpgsql.Datas;

internal class MyEntityDbConfiguration : IEntityTypeConfiguration<MyEntity>
{
    public void Configure(EntityTypeBuilder<MyEntity> builder)
    {
        builder.OwnsMany(x => x.JsonbFields, r => { r.ToJson(); });
    }
}