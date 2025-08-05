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
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(
            ILogger<ItemsController> logger
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
                    .Select(x => new Item
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SerialNumber = x.SerialNumber,
                        ItemType = new ItemType
                        {
                            Id = x.ItemType.Id,
                            Name = x.ItemType.Name
                        },
                        Price = x.Price,
                        Description = x.Description,
                        PurchaseDate = x.PurchaseDate,
                        HolderEmployee = (x.HolderEmployeeId == null) 
                            ? null 
                            : new Employee {
                                Id = x.HolderEmployeeId.Value
                            }
                    })
                    .ToList()
            };
        }

        /// <summary>
        ///     Adds item
        /// </summary>
        /// <param name="createItemRequest"></param>
        [RequiresPermission(UserClaimsProvider.CanManageItems)]
        [HttpPost]
        public async Task<CreateItemResponse> CreatCreateItemAsync(
            [FromServices] CreateItemCommand createCreateItemCommand,
            [Required][FromBody] CreateItemRequest createItemRequest
        )
        {
            var createItemCommandParams = new CreateItemCommandParams
            {
                Name = createItemRequest.Name,
                SerialNumber = createItemRequest.SerialNumber,
                ItemTypeId = createItemRequest.ItemTypeId,
                Price = createItemRequest.Price,
                Description = createItemRequest.Description,
                PurchaseDate = createItemRequest.PurchaseDate,
                HolderEmployeeId = createItemRequest.HolderEmployeeId

            };

            var newCreateItemId = await createCreateItemCommand.ExecuteAsync(createItemCommandParams, User.GetTenantId());

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
