using System.ComponentModel.DataAnnotations;

namespace Api.Requests;

public class CreateItemTypeRequest
{
    [MaxLength(128)]
    [Required]
    public required string Name { get; set; }
}
