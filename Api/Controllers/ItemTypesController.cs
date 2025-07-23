using Api.Requests;
using Api.Responses;
using Application.Commands;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("item-types")]
    public class ItemTypesController : ControllerBase
    {
        private readonly ILogger<ItemTypesController> _logger;

        private readonly CreateItemTypeCommand _createItemTypeCommand;
        private readonly DeleteItemTypeCommand _deleteItemTypeCommand;
        private readonly GetAllItemTypesQuery _getAllItemTypeQuery;

        public ItemTypesController(ILogger<ItemTypesController> logger,
            CreateItemTypeCommand createItemTypeCommand,
            DeleteItemTypeCommand deleteItemTypeCommand,
            GetAllItemTypesQuery getAllItemTypeQuery
            )
        {
            _createItemTypeCommand = createItemTypeCommand;
            _deleteItemTypeCommand = deleteItemTypeCommand;
            _getAllItemTypeQuery = getAllItemTypeQuery;
            _logger = logger;
        }

        /// <summary>
        ///     Get all item types
        /// </summary>
        [HttpGet]
        public async Task<ItemTypesListResponse> GetAllItemTypesAsync()
        {
            var itemTypes = await _getAllItemTypeQuery.GetAsync();

            return new ItemTypesListResponse
            {
                ItemTypes = itemTypes
                    .Select(x => new ItemTypeListItem
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
        [HttpPost]
        public async Task<CreateItemTypeResponse> CreatItemTypeAsync([Required][FromBody] CreateItemTypeRequest createItemTypeRequest)
        {
            var createItemTypeCommandParams = new CreateItemTypeCommandParams
            {
                Name = createItemTypeRequest.Name
            };

            var newItemTypeId = await _createItemTypeCommand.ExecuteAsync(createItemTypeCommandParams);

            return new CreateItemTypeResponse()
            {
                NewItemTypeId = newItemTypeId
            };
        }

        /// <summary>
        ///     Deletes specific item type
        /// </summary>
        /// <param name="itemTypeId"></param>
        [HttpDelete("{itemTypeId}/hard-delete")]
        public async Task<object> HardDeleteItemType([Required][FromRoute] long itemTypeId)
        {
            await _deleteItemTypeCommand.ExecuteAsync(itemTypeId);

            return new { 
                isDeleted = true
            };
        }
    }
}
