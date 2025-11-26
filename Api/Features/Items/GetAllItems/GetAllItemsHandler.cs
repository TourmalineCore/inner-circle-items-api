using Api.EnternalDeps.EmployeesApi;
using Api.Responses;
using Application.Queries;

namespace Api.Features.Items.GetAllItems;

public class GetAllItemsHandler
{
    private readonly AllItemsQuery _allItemsQuery;
    private readonly EmployeesApi _employeesApi;

    public GetAllItemsHandler(
        AllItemsQuery allItemsQuery,
        EmployeesApi employeesApi
    )
    {
        _allItemsQuery = allItemsQuery;
        _employeesApi = employeesApi;
    }

    public async Task<ItemsResponse> HandleAsync()
    {
        var items = await _allItemsQuery.GetAsync();

        var allEmployeesResponse = await _employeesApi.GetAllEmployeesAsync();

        return new ItemsResponse
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
