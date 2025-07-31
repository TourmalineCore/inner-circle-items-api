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
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;

        public ItemController(
            ILogger<ItemController> logger
        )
        {
            _logger = logger;
        }

        /// <summary>
        ///     Get all items
        /// </summary>
        [RequiresPermission(UserClaimsProvider.CanViewItems)]
        [HttpGet]
        public async Task<ItemsListResponse> GetAllItemsAsync(
            [FromServices] GetAllItemsQuery getAllItemsQuery
        )
        {
            var items = await getAllItemsQuery.GetAsync(User.GetTenantId());

            return new ItemsListResponse
            {
                Items = items
                    .Select(x => new ItemsListItem
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SerialNumber = x.SerialNumber,
                        ItemType = new ItemTypeListItem
                        {
                            Id = x.ItemType.Id,
                            Name = x.ItemType.Name
                        },
                        Price = x.Price,
                        PurchaseDate = x.PurchaseDate,
                        HolderEmployee = (x.HolderId == null) ? null : new Employee {Id = x.HolderId}
                    })
                    .ToList()
            };
        }

        /// <summary>
        ///     Adds item type
        /// </summary>
        /// <param name="createItemRequest"></param>
        [RequiresPermission(UserClaimsProvider.CanManageItems)]
        [HttpPost]
        public async Task<CreateItemResponse> CreatCreateItemAsync(
            [FromServices] CreateItemCommand createCreateItemCommand,
            [Required][FromBody] CreateItemRequest createItemRequest
        )
        {
            var createCreateItemCommandParams = new CreateItemCommandParams
            {
                Name = createItemRequest.Name,
                SerialNumber = createItemRequest.SerialNumber,
                ItemTypeId = createItemRequest.ItemTypeId,
                Price = createItemRequest.Price,
                PurchaseDate = createItemRequest.PurchaseDate,
                HolderId = createItemRequest.HolderId

            };

            var newCreateItemId = await createCreateItemCommand.ExecuteAsync(createCreateItemCommandParams, User.GetTenantId());

            return new CreateItemResponse()
            {
                NewItemId = newCreateItemId
            };
        }

        /// <summary>
        ///     Deletes specific item
        /// </summary>
        /// <param name="itemId"></param>
        [RequiresPermission(UserClaimsProvider.AUTO_TESTS_ONLY_IsItemHardDeleteAllowed)]
        [HttpDelete("{itemId}/hard-delete")]
        public async Task<object> HardDeleteItem(
            [FromServices] DeleteItemCommand deleteItemCommand,
            [Required][FromRoute] long itemId
        )
        {
            await deleteItemCommand.ExecuteAsync(itemId, User.GetTenantId());

            return new
            {
                isDeleted = true
            };
        }

    }
}
