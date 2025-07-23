using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetAllItemTypesQuery
    {
        private readonly AppDbContext _context;

        public GetAllItemTypesQuery(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<ItemType>> GetAsync()
        {
            return _context
                .ItemTypes
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
