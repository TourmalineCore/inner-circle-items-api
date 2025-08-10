using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class HardDeleteEntityCommandTests
{
    [Fact]
    public async Task DeleteExistingEntityTwice_ShouldDeleteEntityFromDbSetAndDoNotThrowAtSecondTime()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteExistingEntityTwice_ShouldDeleteEntityFromDbSetAndDoNotThrowAtSecondTime", x => x.EnableNullChecks(false))
            .Options;

        var appDbContext = new AppDbContext(options);

        await appDbContext
            .Items
            .AddAsync(new Item
            {
                Id = 1,
                TenantId = 777
            });

        await appDbContext.SaveChangesAsync();

        var hardDeleteEntityCommand = new HardDeleteEntityCommand(appDbContext);

        await hardDeleteEntityCommand.ExecuteAsync<Item>(1, 777);

        var itemDoesNotExist = await appDbContext
            .Items
            .AllAsync(x => x.Id != 1);

        Assert.True(itemDoesNotExist);

        // try to delete again
        Assert.Null(await Record.ExceptionAsync(() => hardDeleteEntityCommand.ExecuteAsync<Item>(1, 777)));
    }

    [Fact]
    public async Task DeleteNonExistingEntity_ShouldNotThrowException()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteNonExistingEntity_ShouldNotThrowException", x => x.EnableNullChecks(false))
            .Options;

        var appDbContext = new AppDbContext(options);

        await appDbContext
            .Items
            .AddAsync(new Item
            {
                Id = 1,
                TenantId = 777
            });

        await appDbContext.SaveChangesAsync();

        var hardDeleteEntityCommand = new HardDeleteEntityCommand(appDbContext);

        // try to delete a non-existent item from the same tenant
        Assert.Null(await Record.ExceptionAsync(() => hardDeleteEntityCommand.ExecuteAsync<Item>(2, 777)));

        // try to delete an existent item from a wrong tenant
        Assert.Null(await Record.ExceptionAsync(() => hardDeleteEntityCommand.ExecuteAsync<Item>(1, 888)));
    }
}
