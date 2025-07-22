using Api.Requests;
using Api.Responses;
using Core;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("item-types")]
    public class ItemTypesController : ControllerBase
    {
        private readonly ILogger<ItemTypesController> _logger;

        private static long _nextItemTypeId = 1;
        private static List<ItemType> _itemTypes = new List<ItemType>();

        public ItemTypesController(ILogger<ItemTypesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Get all item types
        /// </summary>
        [HttpGet]
        public async Task<ItemTypesListResponse> GetAllItemTypesAsync()
        {
            return new ItemTypesListResponse
            {
                ItemTypes = _itemTypes
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
            var newItemType = new ItemType
            {
                Id = _nextItemTypeId++,
                Name = createItemTypeRequest.Name
            };

            _itemTypes.Add(newItemType);

            return new CreateItemTypeResponse()
            {
                NewItemTypeId = newItemType.Id
            };
        }

        /// <summary>
        ///     Deletes specific item type
        /// </summary>
        /// <param name="itemTypeId"></param>
        [HttpDelete("{itemTypeId}/hard-delete")]
        public async Task<object> HardDeleteItemType([Required][FromRoute] long itemTypeId)
        {
            _itemTypes = _itemTypes
                .Where(x => x.Id != itemTypeId)
                .ToList();

            return new { isDeleted = true };
        }

    }
}
