using System.ComponentModel.DataAnnotations;
using Api.EnternalDeps.EmployeesApi;
using Api.Requests;
using Api.Responses;
using Application.Commands;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        /// <summary>
        ///     Get all items
        /// </summary>
        [RequiresPermission(UserClaimsProvider.CanViewItems)]
        [HttpGet]
        public async Task<ItemsResponse> GetAllItemsAsync(
            [FromServices] AllItemsQuery allItemsQuery,
            [FromServices] EmployeesApi employeesApi
        )
        {
            var items = await allItemsQuery.GetAsync();

            var allEmployeesResponse = await employeesApi.GetAllEmployeesAsync();

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
                            : new EmployeeDto
                            {
                                Id = x.HolderEmployeeId.Value,
                                FullName = allEmployeesResponse
                                    .Employees
                                    .SingleOrDefault(y => y.Id == x.HolderEmployeeId.Value)
                                    ?.FullName ?? "Not Found"
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

            var newItemId = await createItemCommand.ExecuteAsync(createItemCommandParams);

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
}
