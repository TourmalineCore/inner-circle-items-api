
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class CreateItemCommandParams
{
    public string Name { get; set; }

    public string SerialNumber { get; set; }

    public long ItemTypeId { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public long? HolderEmployeeId { get; set; }
}

public class CreateItemCommand
{
    private readonly AppDbContext _context;
    private readonly IClaimsProvider _claimsProvider;

    public CreateItemCommand(
        AppDbContext context,
        IClaimsProvider claimsProvider
    )
    {
        _context = context;
        _claimsProvider = claimsProvider;
    }

    public async Task<long> ExecuteAsync(CreateItemCommandParams createItemCommandParams)
    {
        var itemTypeIdDoesNotExistWithinTenant = await _context
            .ItemTypes
            .Where(x => x.TenantId == _claimsProvider.TenantId)
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

        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();

        return item.Id;
    }
}
