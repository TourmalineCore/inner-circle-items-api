using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Items.CreateItem;

public class CreateItemHandler
{
    private readonly TenantAppDbContext _context;
    private readonly IClaimsProvider _claimsProvider;

    public CreateItemHandler(
        TenantAppDbContext context,
        IClaimsProvider claimsProvider
    )
    {
        _context = context;
        _claimsProvider = claimsProvider;
    }

    public async Task<CreateItemResponse> HandleAsync(CreateItemRequest createItemRequest)
    {
        var itemTypeIdDoesNotExistWithinTenant = await _context
            .QueryableWithinTenant<ItemType>()
            .AllAsync(x => x.Id != createItemRequest.ItemTypeId);

        if (itemTypeIdDoesNotExistWithinTenant)
        {
            throw new Exception($"Passed item type where id={createItemRequest.ItemTypeId} is not found within tenant where id={_claimsProvider.TenantId}");
        }

        var item = new Item
        {
            TenantId = _claimsProvider.TenantId,
            Name = createItemRequest.Name,
            SerialNumber = createItemRequest.SerialNumber,
            ItemTypeId = createItemRequest.ItemTypeId,
            Price = createItemRequest.Price,
            Description = createItemRequest.Description,
            PurchaseDate = createItemRequest.PurchaseDate,
            HolderEmployeeId = createItemRequest.HolderEmployeeId
        };

        await _context
            .Items
            .AddAsync(item);

        await _context.SaveChangesAsync();

        return new CreateItemResponse()
        {
            NewItemId = item.Id
        };
    }
}
