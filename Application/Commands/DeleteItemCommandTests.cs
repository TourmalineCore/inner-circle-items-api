using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class DeleteItemCommandTests
{
    [Fact]
    public async Task DeleteExistingItemTwice_ShouldDeleteItemFromDbSetAndDoNotThrowAtSecondTime()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteExistingItem_ShouldDeleteItemFromDbSet", x => x.EnableNullChecks(false))
            .Options;

        var appDbContext = new AppDbContext(options);

        await appDbContext.Items.AddAsync(new Item
        {
            Id = 1,
            TenantId = 777
        });

        await appDbContext.SaveChangesAsync();

        var deleteItemCommand = new DeleteItemCommand(appDbContext);

        await deleteItemCommand.ExecuteAsync(1, 777);

        var itemDoesNotExist = await appDbContext
            .Items
            .AllAsync(x => x.Id != 1);

        Assert.True(itemDoesNotExist);

        // try to delete again
        Assert.Null(await Record.ExceptionAsync(() => deleteItemCommand.ExecuteAsync(1, 777)));
    }

    [Fact]
    public async Task DeleteNonExistingItem_ShouldNotThrowException()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteNonExistingItem_ShouldNotThrowException", x => x.EnableNullChecks(false))
            .Options;

        var appDbContext = new AppDbContext(options);

        await appDbContext.Items.AddAsync(new Item
        {
            Id = 1,
            TenantId = 777
        });

        await appDbContext.SaveChangesAsync();

        var deleteItemCommand = new DeleteItemCommand(appDbContext);

        // try to delete a non-existent item from the same tenant
        Assert.Null(await Record.ExceptionAsync(() => deleteItemCommand.ExecuteAsync(2, 777)));

        // try to delete an existent item from a wrong tenant
        Assert.Null(await Record.ExceptionAsync(() => deleteItemCommand.ExecuteAsync(1, 888)));
    }
}
