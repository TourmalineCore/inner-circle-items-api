using Application.Features.Items.CreateItem;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class CreateItemCommand
{
    private readonly TenantAppDbContext _context;
    private readonly IClaimsProvider _claimsProvider;

    public CreateItemCommand(
        TenantAppDbContext context,
        IClaimsProvider claimsProvider
    )
    {
        _context = context;
        _claimsProvider = claimsProvider;
    }

    public async Task<long> ExecuteAsync(CreateItemRequest createItemRequest)
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

        return item.Id;
    }
}