using Application.Features.SharedCommands;
using Core.Entities;

namespace Application.Features.ItemTypes.HardDeleteItemType;

public class HardDeleteItemTypeHandler
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public HardDeleteItemTypeHandler(TenantAppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }
    public Task<bool> HandleAsync(long itemTypeId)
    {
        return _hardDeleteEntityCommand.ExecuteAsync<ItemType>(itemTypeId);
    }
}
