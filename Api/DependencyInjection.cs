using Api.ExternalDeps.EmployeesApi;
using Application;
using Application.ExternalDeps.EmployeesApi;
using Application.Features.Items.CreateItem;
using Application.Features.Items.GetAllItems;
using Application.Features.Items.HardDeleteItem;
using Application.Features.ItemTypes.CreateItemType;
using Application.Features.ItemTypes.GetAllItemTypes;
using Application.Features.ItemTypes.HardDeleteItemType;
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

        services.AddTransient<IEmployeesApi, EmployeesApi>();

        services.AddTransient<CreateItemTypeHandler>();
        services.AddTransient<CreateItemTypeCommand>();

        services.AddTransient<GetAllItemTypesHandler>();
        services.AddTransient<GetAllItemTypesQuery>();

        services.AddTransient<HardDeleteItemTypeHandler>();

        services.AddTransient<CreateItemHandler>();
        services.AddTransient<CreateItemCommand>();

        services.AddTransient<GetAllItemsHandler>();
        services.AddTransient<GetAllItemsQuery>();

        services.AddTransient<HardDeleteItemHandler>();
    }
}
