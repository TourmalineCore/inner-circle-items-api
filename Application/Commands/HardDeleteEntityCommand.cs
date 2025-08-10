using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class HardDeleteEntityCommand
{
    private readonly AppDbContext _context;

    public HardDeleteEntityCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync<TEntity>(long id, long tenantId)
        where TEntity : EntityBase
    {
        var entity = await _context
            .Set<TEntity>()
            .Where(x => x.TenantId == tenantId)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (entity == null)
        {
            return;
        }
        
        _context
            .Set<TEntity>()
            .Remove(entity);

        await _context.SaveChangesAsync();
    }
}
