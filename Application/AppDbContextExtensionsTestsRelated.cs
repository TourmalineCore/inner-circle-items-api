using Core;

namespace Application;

public static class AppDbContextExtensionsTestsRelated
{
    public async static Task AddEntityAndSaveAsync<TEntity>(
        this AppDbContext appDbContext, 
        TEntity newEntity
    )
        where TEntity : EntityBase
    {
        await appDbContext
            .Set<TEntity>()
            .AddAsync(newEntity);

        await appDbContext.SaveChangesAsync();
    }
}
