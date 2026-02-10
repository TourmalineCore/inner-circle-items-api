using Application.SharedDtos;

namespace Application.Features.ItemTypes.GetAllItemTypes;

public class GetAllItemTypesHandler
{
    private readonly GetAllItemTypesQuery _getAllItemTypesQuery;

    public GetAllItemTypesHandler(
        GetAllItemTypesQuery getAllItemTypesQuery
    )
    {
        _getAllItemTypesQuery = getAllItemTypesQuery;
    }

    public async Task<GetAllItemTypesResponse> HandleAsync()
    {
        var itemTypes = await _getAllItemTypesQuery.GetAsync();

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
