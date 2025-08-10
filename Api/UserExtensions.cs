using Application;
using System.Security.Claims;

namespace Api;

public static class UserExtensions
{
    private const string TenantIdClaimType = "tenantId";

    public static long GetTenantId(this ClaimsPrincipal context)
    {
        return long.Parse(context.FindFirstValue(TenantIdClaimType));
    }
}

public class HttpContextClaimsProvider : IClaimsProvider
{
    private const string TenantIdClaimType = "tenantId";

    private long _tenantId;

    public HttpContextClaimsProvider(IHttpContextAccessor httpContext)
    {
        _tenantId = long.Parse(httpContext.HttpContext!.User.FindFirstValue(TenantIdClaimType)!);
    }

    public long TenantId
    {
        get { return _tenantId; }
    }
}