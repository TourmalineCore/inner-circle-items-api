using Core;

namespace Application;

internal static class TenantAppDbContextExtensionsTestsRelated
{
    public async static Task AddEntityAndSaveAsync<TEntity>(
        this TenantAppDbContext appDbContext, 
        TEntity newEntity
    )
        where TEntity : EntityBase
    {
        newEntity.TenantId = TenantAppDbContext.TestsRelatedTenantId;

        await appDbContext
            .Set<TEntity>()
            .AddAsync(newEntity);

        await appDbContext.SaveChangesAsync();
    }
}
