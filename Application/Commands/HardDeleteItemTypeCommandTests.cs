using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class HardDeleteItemTypeCommandTests
{
    [Fact]
    public async Task DeleteItemTypeThatHasRelatedItem_ShouldDeleteItemAsWell()
    {
        var context = TenantAppDbContextExtensionsTestsRelated.CteateInMemoryTenantContextForTests();

        await context.AddEntityAndSaveAsync(new ItemType
        {
            Id = 1
        });

        await context.AddEntityAndSaveAsync(new Item
        {
            Id = 2,
            ItemTypeId = 1
        });

        var deleteItemTypeCommand = new HardDeleteItemTypeCommand(context);

        await deleteItemTypeCommand.ExecuteAsync(1);

        var itemDoesNotExist = await context
            .Items
            .AllAsync(x => x.Id != 2);

        Assert.True(itemDoesNotExist);
    }
}
