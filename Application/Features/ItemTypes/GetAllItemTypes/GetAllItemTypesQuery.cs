using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ItemTypes.GetAllItemTypes;

public class GetAllItemTypesQuery
{
    private readonly TenantAppDbContext _context;

    public GetAllItemTypesQuery(TenantAppDbContext context)
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
