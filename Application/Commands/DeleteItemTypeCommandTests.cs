using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class DeleteItemTypeCommandTests
{
    [Fact]
    public async Task DeleteItemTypeThatHasRelatedItem_ShouldDeleteItemAsWell()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteItemTypeThatHasRelatedItem_ShouldDeleteItemAsWell", x => x.EnableNullChecks(false))
            .Options;

        var appDbContext = new AppDbContext(options);

        await appDbContext
            .ItemTypes
            .AddAsync(new ItemType
            {
                Id = 1,
                TenantId = 777
            });

        await appDbContext
            .Items
            .AddAsync(new Item
            {
                Id = 2,
                TenantId = 777,
                ItemTypeId = 1
            });

        await appDbContext.SaveChangesAsync();

        var deleteItemTypeCommand = new DeleteItemTypeCommand(appDbContext);

        await deleteItemTypeCommand.ExecuteAsync(1, 777);

        var itemDoesNotExist = await appDbContext
            .Items
            .AllAsync(x => x.Id != 2);

        Assert.True(itemDoesNotExist);
    }
}
