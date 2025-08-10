using Application;
using Application.Commands;
using Application.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Api;

public static class DependencyInjection
{
    private const string DefaultConnection = "DefaultConnection";

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // https://stackoverflow.com/a/37373557
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();

        var connectionString = configuration.GetConnectionString(DefaultConnection);

        services.AddDbContext<AppDbContext>(options => { 
            options.UseNpgsql(connectionString); 
        });

        services.AddTransient<CreateItemTypeCommand>();
        services.AddTransient<HardDeleteItemTypeCommand>();
        services.AddTransient<AllItemTypesQuery>();

        services.AddTransient<CreateItemCommand>();
        services.AddTransient<AllItemsQuery>();
        services.AddTransient<HardDeleteItemCommand>();
    }
}