using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetAllItemTypeQuery
    {
        private readonly AppDbContext _context;

        public GetAllItemTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemType>> GetAllAsync()
        {
            var itemTypeList = await _context.ItemTypes
                .ToListAsync();
            return itemTypeList;
        }
    }
}
