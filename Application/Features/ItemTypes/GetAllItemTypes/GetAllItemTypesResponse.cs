using Application.SharedDtos;

namespace Application.Features.ItemTypes.GetAllItemTypes;

public class GetAllItemTypesResponse
{
    public required List<ItemTypeDto> ItemTypes { get; set; }
}
