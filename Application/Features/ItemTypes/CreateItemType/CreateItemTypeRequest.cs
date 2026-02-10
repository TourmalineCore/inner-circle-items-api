using System.ComponentModel.DataAnnotations;

namespace Application.Features.ItemTypes.CreateItemType;

public class CreateItemTypeRequest
{
    [MaxLength(128)]
    [Required]
    public required string Name { get; set; }
}
