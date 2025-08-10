using Core.Entities;

namespace Application.Commands;

public class DeleteItemTypeCommand
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public DeleteItemTypeCommand(AppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }

    public Task<bool> ExecuteAsync(long id, long tenantId)
    {
        return _hardDeleteEntityCommand.ExecuteAsync<ItemType>(id, tenantId);
    }
}
