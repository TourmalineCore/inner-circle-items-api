using Core.Entities;

namespace Application.Commands;

public class HardDeleteItemCommand
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public HardDeleteItemCommand(TenantAppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }

    public Task<bool> ExecuteAsync(long itemId)
    {
        return _hardDeleteEntityCommand.ExecuteAsync<Item>(itemId);
    }
}
