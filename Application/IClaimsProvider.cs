namespace Application;

// https://stackoverflow.com/a/75203625
public interface IClaimsProvider
{
  public long TenantId { get; }
}
