
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

    public CreateItemCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> ExecuteAsync(CreateItemCommandParams createItemCommandParams, long tenantId)
    {
        var itemTypeIdDoesNotExistWithinTenant = await _context
            .ItemTypes
            .Where(x => x.TenantId == tenantId)
            .AllAsync(x => x.Id != createItemCommandParams.ItemTypeId);

        if (itemTypeIdDoesNotExistWithinTenant)
        {
            throw new Exception($"Passed item type where id={createItemCommandParams.ItemTypeId} is not found within tenant where id={tenantId}");
        }

        var item = new Item
        {
            TenantId = tenantId,
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
