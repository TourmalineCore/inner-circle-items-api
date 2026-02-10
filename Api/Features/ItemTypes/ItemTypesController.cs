using System.ComponentModel.DataAnnotations;
using Application.Features.ItemTypes.CreateItemType;
using Application.Features.ItemTypes.GetAllItemTypes;
using Application.Features.ItemTypes.HardDeleteItemType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Features.ItemTypes;

[Authorize]
[ApiController]
[Route("api/item-types")]
public class ItemTypesController : ControllerBase
{
    /// <summary>
    ///     Get all item types
    /// </summary>
    [RequiresPermission(UserClaimsProvider.CanViewItemsTypes)]
    [HttpGet]
    public Task<GetAllItemTypesResponse> GetAllItemTypesAsync(
        [FromServices] GetAllItemTypesHandler getAllItemTypesHandler
    )
    {
        return getAllItemTypesHandler.HandleAsync();
    }

    /// <summary>
    ///     Adds item type
    /// </summary>
    /// <param name="createItemTypeRequest"></param>
    [RequiresPermission(UserClaimsProvider.CanManageItemsTypes)]
    [HttpPost]
    public Task<CreateItemTypeResponse> CreateItemTypeAsync(
        [FromServices] CreateItemTypeHandler createItemTypeHandler,
        [Required][FromBody] CreateItemTypeRequest createItemTypeRequest
    )
    {
        return createItemTypeHandler.HandleAsync(createItemTypeRequest);
    }

    /// <summary>
    ///     Deletes specific item type
    /// </summary>
    /// <param name="itemTypeId"></param>
    [RequiresPermission(UserClaimsProvider.AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed)]
    [HttpDelete("{itemTypeId}/hard-delete")]
    public async Task<object> HardDeleteItemTypeAsync(
        [FromServices] HardDeleteItemTypeHandler hardDeleteItemTypeHandler,
        [Required][FromRoute] long itemTypeId
    )
    {
        return new
        {
            isDeleted = await hardDeleteItemTypeHandler.HandleAsync(itemTypeId)
        };
    }
}
