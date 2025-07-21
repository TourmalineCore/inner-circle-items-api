using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("item-types")]
    public class ItemTypesController : ControllerBase
    {
        private readonly ILogger<ItemTypesController> _logger;

        public ItemTypesController(ILogger<ItemTypesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<ItemType> Get()
        {
            return new List<ItemType>() { 
                new ItemType
                {
                    Name = "Test"
                }
            };
        }
    }
}
