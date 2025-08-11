using Core.Entities;

namespace Application.Commands;

public class CreateItemTypeCommandParams
{
    public string Name { get; set; }
}

public class CreateItemTypeCommand
{
    private readonly TenantAppDbContext _context;
    private readonly IClaimsProvider _claimsProvider;

    public CreateItemTypeCommand(
        TenantAppDbContext context,
        IClaimsProvider claimsProvider
    )
    {
        _context = context;
        _claimsProvider = claimsProvider;
    }

    public async Task<long> ExecuteAsync(CreateItemTypeCommandParams createItemTypeCommandParams)
    {
        var itemType = new ItemType
        {
            TenantId = _claimsProvider.TenantId,
            Name = createItemTypeCommandParams.Name
        };

        await _context
            .ItemTypes
            .AddAsync(itemType);

        await _context.SaveChangesAsync();

        return itemType.Id;
    }
}
