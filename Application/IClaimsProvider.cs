namespace Application;

// https://stackoverflow.com/a/75203625
public interface IClaimsProvider
{
    long TenantId { get; }
}
