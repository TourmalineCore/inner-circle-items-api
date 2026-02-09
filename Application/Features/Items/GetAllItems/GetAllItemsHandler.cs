using Application.ExternalDeps.EmployeesApi;
using Application.Features.Dtos;
using Application.Queries;

namespace Application.Features.Items.GetAllItems;

public class GetAllItemsHandler
{
    private readonly AllItemsQuery _allItemsQuery;

    public GetAllItemsHandler(
        AllItemsQuery allItemsQuery
    )
    {
        _allItemsQuery = allItemsQuery;
    }

    public async Task<GetAllItemsResponse> HandleAsync(EmployeesResponse allEmployeesResponse)
    {
        var items = await _allItemsQuery.GetAsync();

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
