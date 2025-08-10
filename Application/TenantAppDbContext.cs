using Core;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class TenantAppDbContext : AppDbContext
{
    private readonly long _tenantId;

    internal static long TestsRelatedTenantId = 777;

    public TenantAppDbContext(
        DbContextOptions<AppDbContext> options,
        IClaimsProvider claimsProvider
    )
    : base(options)
    {
        _tenantId = claimsProvider.TenantId;
    }

    // tests related constructor
    internal TenantAppDbContext(
        DbContextOptions<AppDbContext> options,
        long tenantId
    )
    : base(options)
    {
        _tenantId = tenantId;
    }

    public IQueryable<TEntity> QueryableWithinTenant<TEntity>()
        where TEntity : EntityBase
    {
        return Set<TEntity>()
            .Where(x => x.TenantId == _tenantId);
    }

    public IQueryable<TEntity> QueryableWithinTenantAsNoTracking<TEntity>()
        where TEntity : EntityBase
    {
        return QueryableWithinTenant<TEntity>()
            .AsNoTracking();
    }

    internal static TenantAppDbContext CteateInMemoryTenantContextForTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(
                // we need a unique db name so that tests of the same collection can run in isolation
                // otherwise they inrefere and see each others data
                new Random(100500).Next().ToString(),
                // we want to provide as little setup data as possible to check a certain piece of a flow
                // thus, we don't want to specify all properties of seeded data when it isn't used by the logic
                // for instance, I need to check that an entity exists by Id, I don't need to setup its required Name property
                // this option allows me to bypass requited non-nullable Name check
                x => x.EnableNullChecks(false)
            )
            .Options;

        return new TenantAppDbContext(
            options,
            TestsRelatedTenantId
        );
    }
}
