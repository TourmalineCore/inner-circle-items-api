using Application.Features.SharedCommands;
using Core.Entities;

namespace Application.Features.Items.HardDeleteItem;

public class HardDeleteItemHandler
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public HardDeleteItemHandler(TenantAppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }

    public Task<bool> HandleAsync(long itemId)
    {
        return _hardDeleteEntityCommand.ExecuteAsync<Item>(itemId);
    }
}
