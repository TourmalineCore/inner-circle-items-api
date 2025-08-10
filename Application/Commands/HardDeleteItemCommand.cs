using Core.Entities;

namespace Application.Commands;

public class HardDeleteItemCommand
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public HardDeleteItemCommand(AppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }

    public Task<bool> ExecuteAsync(long id, long tenantId)
    {
        return _hardDeleteEntityCommand.ExecuteAsync<Item>(id, tenantId);
    }
}
