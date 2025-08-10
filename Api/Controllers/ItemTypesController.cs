using Api.Requests;
using Api.Responses;
using Application.Commands;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("item-types")]
    public class ItemTypesController : ControllerBase
    {
        /// <summary>
        ///     Get all item types
        /// </summary>
        [RequiresPermission(UserClaimsProvider.CanViewItemsTypes)]
        [HttpGet]
        public async Task<ItemTypesResponse> GetAllItemTypesAsync(
            [FromServices] AllItemTypesQuery allItemTypesQuery
        )
        {
            var itemTypes = await allItemTypesQuery.GetAsync(User.GetTenantId());

            return new ItemTypesResponse
            {
                ItemTypes = itemTypes
                    .Select(x => new ItemTypeDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList()
            };
        }

        /// <summary>
        ///     Adds item type
        /// </summary>
        /// <param name="createItemTypeRequest"></param>
        [RequiresPermission(UserClaimsProvider.CanManageItemsTypes)]
        [HttpPost]
        public async Task<CreateItemTypeResponse> CreatItemTypeAsync(
            [FromServices] CreateItemTypeCommand createItemTypeCommand,
            [Required][FromBody] CreateItemTypeRequest createItemTypeRequest
        )
        {
            var createItemTypeCommandParams = new CreateItemTypeCommandParams
            {
                Name = createItemTypeRequest.Name
            };

            var newItemTypeId = await createItemTypeCommand.ExecuteAsync(createItemTypeCommandParams, User.GetTenantId());

            return new CreateItemTypeResponse()
            {
                NewItemTypeId = newItemTypeId
            };
        }

        /// <summary>
        ///     Deletes specific item type
        /// </summary>
        /// <param name="itemTypeId"></param>
        [RequiresPermission(UserClaimsProvider.AUTO_TESTS_ONLY_IsItemTypesHardDeleteAllowed)]
        [HttpDelete("{itemTypeId}/hard-delete")]
        public async Task<object> HardDeleteItemType(
            [FromServices] HardDeleteItemTypeCommand hardDeleteItemTypeCommand,
            [Required][FromRoute] long itemTypeId
        )
        {
            return new { 
                isDeleted = await hardDeleteItemTypeCommand.ExecuteAsync(itemTypeId, User.GetTenantId())
            };
        }
    }
}
