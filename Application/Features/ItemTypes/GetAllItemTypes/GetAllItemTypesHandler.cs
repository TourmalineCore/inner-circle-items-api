using Application.Features.Dtos;
using Application.Queries;

namespace Application.Features.ItemTypes.GetAllItemTypes;

public class GetAllItemTypesHandler
{
    private readonly AllItemTypesQuery _allItemTypesQuery;

    public GetAllItemTypesHandler(
        AllItemTypesQuery allItemTypesQuery
    )
    {
        _allItemTypesQuery = allItemTypesQuery;
    }

    public async Task<ItemTypesResponse> HandleAsync()
    {
        var itemTypes = await _allItemTypesQuery.GetAsync();

        return new ItemTypesResponse
        {
            ItemTypes = itemTypes
                .Select(x => new ItemTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList()
        };
    }
}
