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
        public async Task<ItemsResponse> GetAllItemsAsync(
            [FromServices] AllItemsQuery allItemsQuery
        )
        {
            var items = await allItemsQuery.GetAsync(User.GetTenantId());

            return new ItemsResponse
            {
                Items = items
                    .Select(x => new ItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SerialNumber = x.SerialNumber,
                        ItemType = new ItemTypeDto
                        {
                            Id = x.ItemType.Id,
                            Name = x.ItemType.Name
                        },
                        Price = x.Price,
                        Description = x.Description,
                        PurchaseDate = x.PurchaseDate,
                        HolderEmployee = (x.HolderEmployeeId == null) 
                            ? null 
                            : new EmployeeDto {
                                Id = x.HolderEmployeeId.Value
                            }
                    })
                    .ToList()
            };
        }

        /// <summary>
        ///     Add an item
        /// </summary>
        /// <param name="createItemRequest"></param>
        [RequiresPermission(UserClaimsProvider.CanManageItems)]
        [HttpPost]
        public async Task<CreateItemResponse> CreateItemAsync(
            [FromServices] CreateItemCommand createItemCommand,
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

            var newItemId = await createItemCommand.ExecuteAsync(createItemCommandParams, User.GetTenantId());

            return new CreateItemResponse()
            {
                NewItemId = newItemId
            };
        }

        /// <summary>
        ///     Deletes specific item
        /// </summary>
        /// <param name="itemId"></param>
        [RequiresPermission(UserClaimsProvider.AUTO_TESTS_ONLY_IsItemsHardDeleteAllowed)]
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
