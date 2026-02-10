using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Items.GetAllItems;

public class GetAllItemsQuery
{
    private readonly TenantAppDbContext _context;

    public GetAllItemsQuery(TenantAppDbContext context)
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
