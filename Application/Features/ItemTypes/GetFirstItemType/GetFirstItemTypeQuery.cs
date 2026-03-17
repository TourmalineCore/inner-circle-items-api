using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ItemTypes.GetFirstItemType;

public class GetFirstItemTypeQuery
{
    private readonly TenantAppDbContext _context;

    public GetFirstItemTypeQuery(TenantAppDbContext context)
    {
        _context = context;
    }

    public Task<ItemType?> GetAsync()
    {
        return _context
            .QueryableWithinTenantAsNoTracking<ItemType>()
            .FirstOrDefaultAsync();
    }
}
