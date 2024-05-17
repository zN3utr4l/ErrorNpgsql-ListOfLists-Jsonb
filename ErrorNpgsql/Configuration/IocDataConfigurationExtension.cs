using ErrorNpgsql.Datas;
using Microsoft.EntityFrameworkCore;

namespace ErrorNpgsql.Configuration;

/// <summary>
/// 
/// </summary>
public static class IocDataConfigurationExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataLayerRunTime(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext(configuration);
    }


    internal static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CustomDbContext>(config =>
        {
            config.EnableSensitiveDataLogging();
            config.UseLazyLoadingProxies();
            config.EnableDetailedErrors();
            config.UseSnakeCaseNamingConvention();
            config.UseNpgsql(connectionString);
        });
        return services;
    }
}