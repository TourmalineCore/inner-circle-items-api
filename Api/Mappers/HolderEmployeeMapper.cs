using Api.EnternalDeps.EmployeesApi.Responses;
using Api.Responses;

namespace Api.Mappers;

public class HolderEmployeeMapper
{
    public const string NotFoundEmployeeFullName = "Not Found";

    public static EmployeeDto? MapToEmployeeDto(long? holderEmployeeId, EmployeesResponse employeesResponse)
    {
        return holderEmployeeId == null
            ? null
            : new EmployeeDto
            {
                Id = holderEmployeeId.Value,
                FullName = employeesResponse
                    .Employees
                    .SingleOrDefault(y => y.Id == holderEmployeeId.Value)
                    ?.FullName ?? NotFoundEmployeeFullName
            };
    }
}
