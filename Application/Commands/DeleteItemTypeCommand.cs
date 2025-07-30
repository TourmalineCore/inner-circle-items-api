using Microsoft.EntityFrameworkCore;

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
        var itemType = await _context
            .ItemTypes
            .Where(x => x.TenantId == tenantId)
            .SingleAsync(x => x.Id == id);
            
        _context
            .ItemTypes
            .Remove(itemType);

        await _context.SaveChangesAsync();
    }
}
