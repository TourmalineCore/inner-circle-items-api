using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class HardDeleteEntityCommandTests
{
    [Fact]
    public async Task DeleteExistingEntityTwice_ShouldDeleteEntityFromDbSetAndDoNotThrowAtSecondTime()
    {
        var appDbContext = AppDbContext.CteateInMemoryContextForTests();

        await appDbContext.AddEntityAndSaveAsync(new Item
        {
            Id = 1,
            TenantId = 777
        });

        var hardDeleteEntityCommand = new HardDeleteEntityCommand(appDbContext);

        var wasDeleted = await hardDeleteEntityCommand.ExecuteAsync<Item>(1);

        var itemDoesNotExist = await appDbContext
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
        var appDbContext = AppDbContext.CteateInMemoryContextForTests();

        await appDbContext.AddEntityAndSaveAsync(new Item
        {
            Id = 1,
            TenantId = 777
        });

        var hardDeleteEntityCommand = new HardDeleteEntityCommand(appDbContext);

        var wasNonExistedDeleted = true;

        // try to delete a non-existent item
        Assert.Null(await Record.ExceptionAsync(async () => wasNonExistedDeleted = await hardDeleteEntityCommand.ExecuteAsync<Item>(2)));
        Assert.False(wasNonExistedDeleted);
    }
}
