
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class DeleteItemTypeCommand
{
    private readonly AppDbContext _context;

    public DeleteItemTypeCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(long id)
    {
        var itemType = _context.ItemTypes
            .Where(x => x.Id == id)
            .SingleOrDefault();

        if (itemType != null)
        {
            _context.ItemTypes.Remove(itemType);
            await _context.SaveChangesAsync();
        }
        
    }
}
