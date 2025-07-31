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

    }
}
