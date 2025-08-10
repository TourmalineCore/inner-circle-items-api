﻿using Application;
using Application.Commands;
using Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class DependencyInjection
{
    private const string DefaultConnection = "DefaultConnection";

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DefaultConnection);

        services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(connectionString); }
        );
        services.AddTransient<CreateItemTypeCommand>();
        services.AddTransient<HardDeleteItemTypeCommand>();
        services.AddTransient<AllItemTypesQuery>();

        services.AddTransient<CreateItemCommand>();
        services.AddTransient<AllItemsQuery>();
        services.AddTransient<HardDeleteItemCommand>();
    }
}