namespace Api.ExternalDeps.EmployeesApi.Responses;

public class EmployeesResponse
{
    public required List<EmployeeDto> Employees { get; set; }

    public class EmployeeDto
    {
        public long Id { get; set; }

        public required string FullName { get; set; }

        public required string CorporateEmail { get; set; }

        public long TenantId { get; set; }
    }
}

