namespace Application.Commands;

public class DeleteItemTypeCommand
{
    private readonly AppDbContext _context;

    public DeleteItemTypeCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync(long id, long tenantId)
    {
        var itemType = _context
            .ItemTypes
            .Single(x => x.Id == id && x.TenantId == tenantId);
            
        _context
            .ItemTypes
            .Remove(itemType);

        await _context.SaveChangesAsync();
    }
}
