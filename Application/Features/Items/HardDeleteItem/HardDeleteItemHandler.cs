using Application.Commands;
using Core.Entities;

namespace Application.Features.Items.HardDeleteItem;

public class HardDeleteItemHandler
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public HardDeleteItemHandler(TenantAppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }

    public async Task<bool> HandleAsync(long itemId)
    {
        return await _hardDeleteEntityCommand.ExecuteAsync<Item>(itemId);
    }
}
