using Core.Entities;

namespace Application.Commands;

public class HardDeleteItemTypeCommand
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public HardDeleteItemTypeCommand(TenantAppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }

    public Task<bool> ExecuteAsync(long itemTypeId)
    {
        return _hardDeleteEntityCommand.ExecuteAsync<ItemType>(itemTypeId);
    }
}
