
using Core.Entities;

namespace Application.Commands;

public class CreateItemTypeCommandParams
{
    public string Name { get; set; }
}

public class CreateItemTypeCommand
{
    private readonly AppDbContext _context;

    public CreateItemTypeCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> ExecuteAsync(CreateItemTypeCommandParams createItemTypeCommandParams, long tenantId)
    {
        var itemType = new ItemType
        {
            TenantId = tenantId,
            Name = createItemTypeCommandParams.Name
        };

        await _context.ItemTypes.AddAsync(itemType);
        await _context.SaveChangesAsync();

        return itemType.Id;
    }
}
