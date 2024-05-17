using ErrorNpgsql.Configuration;
using ErrorNpgsql.Datas;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDataLayerRunTime(configuration);

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/error-jsonb", ([FromServices] CustomDbContext context) => context.MyEntity.ToList()).WithName("ErroJsonb");

app.Run();
