using Application.SharedDtos;

namespace Application.Features.ItemTypes.GetFirstItemType;

public class GetFirstItemTypeHandler
{
    private readonly GetFirstItemTypeQuery _getFirstItemTypeQuery;

    public GetFirstItemTypeHandler(
        GetFirstItemTypeQuery getFirstItemTypeQuery
    )
    {
        _getFirstItemTypeQuery = getFirstItemTypeQuery;
    }

    public async Task<GetFirstItemTypeResponse?> HandleAsync()
    {
        var itemType = await _getFirstItemTypeQuery.GetAsync();

        return itemType == null
            ? null
            : new GetFirstItemTypeResponse
            {
                Id = itemType.Id,
                Name = itemType.Name
            };
    }
}
