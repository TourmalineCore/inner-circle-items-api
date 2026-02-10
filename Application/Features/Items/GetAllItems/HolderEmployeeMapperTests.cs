using Application.ExternalDeps.EmployeesApi;
using Xunit;

namespace Application.Features.Items.GetAllItems;

public class HolderEmployeeMapperTests
{
    [Fact]
    public void MapNonExistentEmployee_ShouldUseNotFoundAsFullName()
    {
        var empltyEmployeesResponse = new EmployeesResponse
        {
            Employees = new List<EmployeesResponse.EmployeeDto>()
        };

        var employeeId = 5;

        var mappedHolderEmployeeDto = HolderEmployeeMapper.MapToEmployeeDto(employeeId, empltyEmployeesResponse);

        Assert.Equal(employeeId, mappedHolderEmployeeDto!.Id);
        Assert.Equal(HolderEmployeeMapper.NotFoundEmployeeFullName, mappedHolderEmployeeDto.FullName);
    }
}
