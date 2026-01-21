using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Api.ExternalDeps.EmployeesApi;
using Application.Commands;
using Application.Features.Items.CreateItem;
using Application.Features.Items.GetAllItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Tags("Tag 1")] // If not set, defaults to "Items"
[Route("api/items")]
public class ItemsController : ControllerBase
{
    [EndpointName("GetAllItems")] // For operationId
    [EndpointSummary("Get all items")]
    [Tags("Tag 2")]
    [EndpointDescription("This is a description.")]
    [RequiresPermission(UserClaimsProvider.CanViewItems)]
    [HttpGet]
    public async Task<GetAllItemsResponse> GetAllItemsAsync(
        [FromServices] GetAllItemsHandler getAllItemsHandler,
        [FromServices] EmployeesApi employeesApi
    )
    {
        var allEmployeesResponse = await employeesApi.GetAllEmployeesAsync();

        return await getAllItemsHandler.HandleAsync(allEmployeesResponse);
    }

    [EndpointSummary("Add an item")]
    /// <param name="createItemRequest"></param>
    [RequiresPermission(UserClaimsProvider.CanManageItems)]
    [HttpPost]
    public Task<CreateItemResponse> CreateItemAsync(
        [FromServices] CreateItemHandler createItemHandler,
        [Required][FromBody] CreateItemRequest createItemRequest
    )
    {
        return createItemHandler.HandleAsync(createItemRequest);
    }

    [EndpointSummary("Deletes specific item")]
    [RequiresPermission(UserClaimsProvider.AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed)]
    [HttpDelete("{itemId}/hard-delete")]
    public async Task<object> HardDeleteItemAsync(
        [FromServices] HardDeleteItemCommand hardDeleteItemCommand,
        [Required][FromRoute, Description("ID of the item to delete")] long itemId
    )
    {
        return new
        {
            isDeleted = await hardDeleteItemCommand.ExecuteAsync(itemId)
        };
    }
}
