using System.ComponentModel.DataAnnotations;
using Application.Features.Items.CreateItem;
using Application.Features.Items.GetAllItems;
using Application.Features.Items.HardDeleteItem;
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
        [FromServices] GetAllItemsHandler getAllItemsHandler
    )
    {
        return await getAllItemsHandler.HandleAsync();
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
        [FromServices] HardDeleteItemHandler hardDeleteItemHandler,
        [Required][FromRoute] long itemId
    )
    {
        return new
        {
            isDeleted = await hardDeleteItemHandler.HandleAsync(itemId)
        };
    }
}
