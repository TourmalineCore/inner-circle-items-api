using Core.Entities;

namespace Application.Commands;

public class DeleteItemCommand
{
    private readonly HardDeleteEntityCommand _hardDeleteEntityCommand;

    public DeleteItemCommand(AppDbContext context)
    {
        _hardDeleteEntityCommand = new HardDeleteEntityCommand(context);
    }

    public Task ExecuteAsync(long id, long tenantId)
    {
        return _hardDeleteEntityCommand.ExecuteAsync<Item>(id, tenantId);
    }
}
