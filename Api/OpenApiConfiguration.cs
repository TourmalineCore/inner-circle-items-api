using Microsoft.AspNetCore.Mvc.Controllers;

namespace Api;

public static class OpenApiConfiguration
{
    public static void AddConfiguredOpenApi(this IServiceCollection services)
    {
        var apiVersion = File
            .ReadLines("../__version")
            .First();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi(options =>
        {
            options.AddOperationTransformer((operation, context, _) =>
            {
                // Try to get the ControllerActionDescriptor to access method information
                if (context.Description.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    // Set the operationId to the ControllerName and ActionName (which is typically the method name)
                    // This allows to have unique operationId even if there is the same method name across multiple controllers
                    operation.OperationId = $"{controllerActionDescriptor.ControllerName}{controllerActionDescriptor.ActionName}";
                }

                return Task.CompletedTask;
            });

            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new()
                {
                    Title = "inner-circle-items-api",
                    Version = apiVersion
                };

                return Task.CompletedTask;
            });
        });
    }
}
