using Application.Features.SharedDtos;

namespace Application.Features.ItemTypes;

public class ItemTypesResponse
{
    public required List<ItemTypeDto> ItemTypes { get; set; }
}
