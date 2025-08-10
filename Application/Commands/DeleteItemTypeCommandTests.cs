using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class DeleteItemTypeCommandTests
{
    [Fact]
    public async Task DeleteExistingItemTypeTwice_ShouldDeleteItemTypeFromDbSetAndDoNotThrowAtSecondTime()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteExistingItemTypeTwice_ShouldDeleteItemTypeFromDbSetAndDoNotThrowAtSecondTime", x => x.EnableNullChecks(false))
            .Options;

        var appDbContext = new AppDbContext(options);

        await appDbContext
            .ItemTypes
            .AddAsync(new ItemType
            {
                Id = 1,
                TenantId = 777
            });

        await appDbContext.SaveChangesAsync();

        var deleteItemTypeCommand = new DeleteItemTypeCommand(appDbContext);

        await deleteItemTypeCommand.ExecuteAsync(1, 777);

        var itemTypeDoesNotExist = await appDbContext
            .ItemTypes
            .AllAsync(x => x.Id != 1);

        Assert.True(itemTypeDoesNotExist);

        // try to delete again
        Assert.Null(await Record.ExceptionAsync(() => deleteItemTypeCommand.ExecuteAsync(1, 777)));
    }

    [Fact]
    public async Task DeleteNonExistingItemType_ShouldNotThrowException()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteNonExistingItemType_ShouldNotThrowException", x => x.EnableNullChecks(false))
            .Options;

        var appDbContext = new AppDbContext(options);

        await appDbContext
            .ItemTypes
            .AddAsync(new ItemType
            {
                Id = 1,
                TenantId = 777
            });

        await appDbContext.SaveChangesAsync();

        var deleteItemTypeCommand = new DeleteItemTypeCommand(appDbContext);

        // try to delete a non-existent item type from the same tenant
        Assert.Null(await Record.ExceptionAsync(() => deleteItemTypeCommand.ExecuteAsync(2, 777)));

        // try to delete an existent item type from a wrong tenant
        Assert.Null(await Record.ExceptionAsync(() => deleteItemTypeCommand.ExecuteAsync(1, 888)));
    }

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
