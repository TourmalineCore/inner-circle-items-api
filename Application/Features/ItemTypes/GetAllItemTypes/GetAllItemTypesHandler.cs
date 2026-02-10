using Application.Features.SharedDtos;
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

    public async Task<GetAllItemTypesResponse> HandleAsync()
    {
        var itemTypes = await _allItemTypesQuery.GetAsync();

        return new GetAllItemTypesResponse
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
