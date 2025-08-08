using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class DeleteItemCommand
{
    private readonly AppDbContext _context;

    public DeleteItemCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync(long id, long tenantId)
    {
        var item = await _context
            .Items
            .Where(x => x.TenantId == tenantId)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            return;
        }
        
        _context
            .Items
            .Remove(item);

        await _context.SaveChangesAsync();
    }
}
