using System.ComponentModel.DataAnnotations;

namespace Api.Requests;

public class CreateItemRequest
{
    [MaxLength(256)]
    [Required]
    public string Name { get; set; }

    [MaxLength(128)]
    public string SerialNumber { get; set; }

    [Required]
    public long ItemTypeId { get; set; }

    [Required]
    public decimal Price { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public long HolderId { get; set; }
}