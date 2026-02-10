using Application.ExternalDeps.EmployeesApi;
using Application.SharedDtos;

namespace Application.Features.Items.GetAllItems;

public class GetAllItemsHandler
{
    private readonly IEmployeesApi _employeesApi;
    private readonly GetAllItemsQuery _getAllItemsQuery;

    public GetAllItemsHandler(
        IEmployeesApi employeesApi,
        GetAllItemsQuery getAllItemsQuery
    )
    {
        _employeesApi = employeesApi;
        _getAllItemsQuery = getAllItemsQuery;
    }

    public async Task<GetAllItemsResponse> HandleAsync()
    {
        var allEmployeesResponse = await _employeesApi.GetAllEmployeesAsync();

        var items = await _getAllItemsQuery.GetAsync();

        return new GetAllItemsResponse
        {
            Items = items
                .Select(x => new ItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    SerialNumber = x.SerialNumber,
                    ItemType = new ItemTypeDto
                    {
                        Id = x.ItemType.Id,
                        Name = x.ItemType.Name
                    },
                    Price = x.Price,
                    Description = x.Description,
                    PurchaseDate = x.PurchaseDate,
                    HolderEmployee = HolderEmployeeMapper.MapToEmployeeDto(x.HolderEmployeeId, allEmployeesResponse)
                })
                .ToList()
        };
    }
}
