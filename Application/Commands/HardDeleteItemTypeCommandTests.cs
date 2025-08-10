using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Commands;

public class HardDeleteItemTypeCommandTests
{
    [Fact]
    public async Task DeleteItemTypeThatHasRelatedItem_ShouldDeleteItemAsWell()
    {
        var tenantAppDbContext = TenantAppDbContextExtensionsTestsRelated.CteateInMemoryTenantContextForTests();

        await tenantAppDbContext.AddEntityAndSaveAsync(new ItemType
        {
            Id = 1
        });

        await tenantAppDbContext.AddEntityAndSaveAsync(new Item
        {
            Id = 2,
            ItemTypeId = 1
        });

        var deleteItemTypeCommand = new HardDeleteItemTypeCommand(tenantAppDbContext);

        await deleteItemTypeCommand.ExecuteAsync(1);

        var itemDoesNotExist = await tenantAppDbContext
            .Items
            .AllAsync(x => x.Id != 2);

        Assert.True(itemDoesNotExist);
    }
}
