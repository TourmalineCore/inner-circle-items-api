using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class HardDeleteItemTypeCommandTests
{
    [Fact]
    public async Task DeleteItemTypeThatHasRelatedItem_ShouldDeleteItemAsWell()
    {
        var appDbContext = AppDbContext.CteateInMemoryContextForTests();

        await appDbContext.AddEntityAndSaveAsync(new ItemType
        {
            Id = 1,
            TenantId = 777
        });

        await appDbContext.AddEntityAndSaveAsync(new Item
        {
            Id = 2,
            TenantId = 777,
            ItemTypeId = 1
        });

        var deleteItemTypeCommand = new HardDeleteItemTypeCommand(appDbContext);

        await deleteItemTypeCommand.ExecuteAsync(1, 777);

        var itemDoesNotExist = await appDbContext
            .Items
            .AllAsync(x => x.Id != 2);

        Assert.True(itemDoesNotExist);
    }
}
