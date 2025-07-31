using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetAllItemsQuery
    {
        private readonly AppDbContext _context;

        public GetAllItemsQuery(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Item>> GetAsync(long tenantId)
        {
            return _context
                .Items
                .Include(x => x.ItemType)
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId)
                .ToListAsync();
        }
    }
}
