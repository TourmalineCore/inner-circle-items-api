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

        public Task<List<ItemType>> GetAsync(long tenantId)
        {
            return _context
                .ItemTypes
                .AsNoTracking()
                .Where(x => x.TenantId == tenantId)
                .ToListAsync();
        }
    }
}
