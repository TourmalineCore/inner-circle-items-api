
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class CreateItemCommandParams
{
    public string Name { get; set; }

    public string SerialNumber { get; set; }

    public long ItemTypeId { get; set; }

    public decimal Price { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public long? HolderId { get; set; }
}

public class CreateItemCommand
{
    private readonly AppDbContext _context;

    public CreateItemCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> ExecuteAsync(CreateItemCommandParams createItemCommandParams, long tenantId)
    {
        var itemType = await _context
            .ItemTypes
            .Where(x => x.TenantId == tenantId)
            .SingleOrDefaultAsync(x => x.Id == createItemCommandParams.ItemTypeId);

        if (itemType == null)
        {
            throw new Exception($"Passed item type where id={createItemCommandParams.ItemTypeId} is not found");
        }

        var Item = new Item
        {
            TenantId = tenantId,
            Name = createItemCommandParams.Name,
            SerialNumber = createItemCommandParams.SerialNumber,
            ItemTypeId = createItemCommandParams.ItemTypeId,
            Price = createItemCommandParams.Price,
            PurchaseDate = createItemCommandParams.PurchaseDate,
            HolderId = createItemCommandParams.HolderId
        };

        await _context.Items.AddAsync(Item);
        await _context.SaveChangesAsync();

        return Item.Id;
    }
}
