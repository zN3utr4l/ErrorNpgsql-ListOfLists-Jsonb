using ErrorNpgsql.Configuration;
using ErrorNpgsql.Datas;
using ErrorNpgsql.Models;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDataLayerRunTime(configuration);

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/error-jsonb", ([FromServices] CustomDbContext context) =>
{
    context.Add(new MyEntity()
    {
        JsonbFields = [
            [new() { Key = "key1", Value = "value1"}, new() { Key = "key2", Value = "value2"}],
            [new() { Key = "key1", Value = "value1"}, new() { Key = "key2", Value = "value2"}]
        ]
    });

    context.SaveChanges();

    return context.MyEntity.ToList();
}).WithName("ErroJsonb");

app.Run();
