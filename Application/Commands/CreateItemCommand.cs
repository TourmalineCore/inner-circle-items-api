
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class CreateItemCommandParams
{
    public required string Name { get; set; }

    public required string SerialNumber { get; set; }

    public required long ItemTypeId { get; set; }

    public required decimal Price { get; set; }

    public required string Description { get; set; }

    public required DateOnly? PurchaseDate { get; set; }

    public required long? HolderEmployeeId { get; set; }
}

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

    public async Task<long> ExecuteAsync(CreateItemCommandParams createItemCommandParams)
    {
        var itemTypeIdDoesNotExistWithinTenant = await _context
            .QueryableWithinTenant<ItemType>()
            .AllAsync(x => x.Id != createItemCommandParams.ItemTypeId);

        if (itemTypeIdDoesNotExistWithinTenant)
        {
            throw new Exception($"Passed item type where id={createItemCommandParams.ItemTypeId} is not found within tenant where id={_claimsProvider.TenantId}");
        }

        var item = new Item
        {
            TenantId = _claimsProvider.TenantId,
            Name = createItemCommandParams.Name,
            SerialNumber = createItemCommandParams.SerialNumber,
            ItemTypeId = createItemCommandParams.ItemTypeId,
            Price = createItemCommandParams.Price,
            Description = createItemCommandParams.Description,
            PurchaseDate = createItemCommandParams.PurchaseDate,
            HolderEmployeeId = createItemCommandParams.HolderEmployeeId
        };

        await _context
            .Items
            .AddAsync(item);

        await _context.SaveChangesAsync();

        return item.Id;
    }
}
