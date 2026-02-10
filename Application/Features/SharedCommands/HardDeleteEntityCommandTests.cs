using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Features.SharedCommands;

public class HardDeleteEntityCommandTests
{
    [Fact]
    public async Task DeleteExistingEntityTwice_ShouldDeleteEntityFromDbSetAndDoNotThrowAtSecondTime()
    {
        var context = TenantAppDbContextExtensionsTestsRelated.CteateInMemoryTenantContextForTests();

        await context.AddEntityAndSaveAsync(new Item
        {
            Id = 1
        });

        var hardDeleteEntityCommand = new HardDeleteEntityCommand(context);

        var wasDeleted = await hardDeleteEntityCommand.ExecuteAsync<Item>(1);

        var itemDoesNotExist = await context
            .Items
            .AllAsync(x => x.Id != 1);

        Assert.True(wasDeleted);
        Assert.True(itemDoesNotExist);

        var wasDeletedAgain = true;

        // try to delete again
        Assert.Null(await Record.ExceptionAsync(async () => wasDeletedAgain = await hardDeleteEntityCommand.ExecuteAsync<Item>(1)));
        Assert.False(wasDeletedAgain);
    }

    [Fact]
    public async Task DeleteNonExistingEntity_ShouldNotThrowException()
    {
        var context = TenantAppDbContextExtensionsTestsRelated.CteateInMemoryTenantContextForTests();

        await context.AddEntityAndSaveAsync(new Item
        {
            Id = 1
        });

        var hardDeleteEntityCommand = new HardDeleteEntityCommand(context);

        var wasNonExistedDeleted = true;

        // try to delete a non-existent item
        Assert.Null(await Record.ExceptionAsync(async () => wasNonExistedDeleted = await hardDeleteEntityCommand.ExecuteAsync<Item>(2)));
        Assert.False(wasNonExistedDeleted);
    }

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

        var hardDeleteEntityCommand = new HardDeleteEntityCommand(context);

        await hardDeleteEntityCommand.ExecuteAsync<ItemType>(1);

        var itemDoesNotExist = await context
            .Items
            .AllAsync(x => x.Id != 2);

        Assert.True(itemDoesNotExist);
    }
}
