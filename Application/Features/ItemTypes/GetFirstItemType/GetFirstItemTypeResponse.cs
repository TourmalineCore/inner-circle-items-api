using Application.SharedDtos;

namespace Application.Features.ItemTypes.GetFirstItemType;

public class GetFirstItemTypeResponse
{
    public required long Id { get; set; }

    public required string Name { get; set; }
}
