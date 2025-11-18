
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;

namespace Api.EnternalDeps;

public class Employee
{
    public long Id { get; set; }

    public required string FullName { get; set; }

    public required string CorporateEmail { get; set; }

    public long TenantId { get; set; }
}

public class EmployeesApi
{
    private readonly HttpClient _client;
    private readonly ExternalDepsUrls _externalDepsUrls;
    private readonly AuthenticationOptions _authenticationOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeesApi(
        IOptions<ExternalDepsUrls> externalDepsUrls,
        IOptions<AuthenticationOptions> authenticationOptions,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _client = new HttpClient();
        _externalDepsUrls = externalDepsUrls.Value;
        _authenticationOptions = authenticationOptions.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        var link = $"{_externalDepsUrls.EmployeesApiRootUrl}/internal/get-employees";

        var headerName = _authenticationOptions.IsDebugTokenEnabled
          ? "X-DEBUG-TOKEN"
          : "Authorization";

        var token = _httpContextAccessor
          .HttpContext!
          .Request
          .Headers[headerName]
          .ToString();

        _client.DefaultRequestHeaders.Add(headerName, token);

        var response = await _client.GetStringAsync(link);

        return JsonConvert.DeserializeObject<List<Employee>>(response)!;
    }
}
