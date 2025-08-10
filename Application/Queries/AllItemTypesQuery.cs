using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class AllItemTypesQuery
    {
        private readonly TenantAppDbContext _context;

        public AllItemTypesQuery(TenantAppDbContext context)
        {
            _context = context;
        }

        public Task<List<ItemType>> GetAsync()
        {
            return _context
                .QueryableWithinTenantAsNoTracking<ItemType>()
                .ToListAsync();
        }
    }
}
