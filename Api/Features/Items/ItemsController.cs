using System.ComponentModel.DataAnnotations;
using Api.Features.Items.Handlers.CreateItem;
using Api.Features.Items.Handlers.GetAllItems;
using Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Features.Items.Handlers;

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
    public Task<ItemsResponse> GetAllItemsAsync(
        [FromServices] GetAllItemsHandler getAllItemsHandler
    )
    {
        return getAllItemsHandler.HandleAsync();
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
