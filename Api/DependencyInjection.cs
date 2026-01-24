using Api.ExternalDeps.EmployeesApi;
using Application;
using Application.Commands;
using Application.Features.Items.CreateItem;
using Application.Features.Items.GetAllItems;
using Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class DependencyInjection
{
    private const string DefaultConnection = "DefaultConnection";

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // https://stackoverflow.com/a/37373557
        services.AddHttpContextAccessor();
        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();

        var connectionString = configuration.GetConnectionString(DefaultConnection);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<TenantAppDbContext>();

        services.Configure<ExternalDepsUrls>(configuration.GetSection(nameof(ExternalDepsUrls)));

        services.AddTransient<EmployeesApi, EmployeesApi>();

        services.AddTransient<CreateItemTypeCommand>();
        services.AddTransient<HardDeleteItemTypeCommand>();
        services.AddTransient<AllItemTypesQuery>();

        services.AddTransient<AllItemsQuery>();
        services.AddTransient<HardDeleteItemCommand>();

        services.AddTransient<GetAllItemsHandler>();
        services.AddTransient<CreateItemHandler>();
    }
}
