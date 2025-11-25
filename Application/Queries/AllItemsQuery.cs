using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class AllItemsQuery
{
    private readonly TenantAppDbContext _context;

    public AllItemsQuery(TenantAppDbContext context)
    {
        _context = context;
    }

    public Task<List<Item>> GetAsync()
    {
        return _context
            .QueryableWithinTenantAsNoTracking<Item>()
            .Include(x => x.ItemType)
            .ToListAsync();
    }
}
