using System.ComponentModel.DataAnnotations;
using Api.ExternalDeps.EmployeesApi;
using Application.Commands;
using Application.Features.Items.CreateItem;
using Application.Features.Items.GetAllItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Features.Items;

[Authorize]
[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    /// <summary>
    ///     Get all items
    /// </summary>
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

    /// <summary>
    ///     Add an item
    /// </summary>
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

    /// <summary>
    ///     Deletes specific item
    /// </summary>
    /// <param name="itemId"></param>
    [RequiresPermission(UserClaimsProvider.AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed)]
    [HttpDelete("{itemId}/hard-delete")]
    public async Task<object> HardDeleteItemAsync(
        [FromServices] HardDeleteItemCommand hardDeleteItemCommand,
        [Required][FromRoute] long itemId
    )
    {
        return new
        {
            isDeleted = await hardDeleteItemCommand.ExecuteAsync(itemId)
        };
    }
}
