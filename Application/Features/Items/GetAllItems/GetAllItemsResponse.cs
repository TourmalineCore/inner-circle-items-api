using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Features.Dtos;

namespace Application.Features.Items.GetAllItems;

public class GetAllItemsResponse
{
    public required List<ItemDto> Items { get; set; }
}

public class ItemDto
{
    [Required]
    public required long Id { get; set; }

    [MaxLength(128)]
    [Description("Description for Name")]
    [DefaultValue("DefaultName")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can contain only letters and spaces.")]
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string SerialNumber { get; set; }

    [Required]
    public required ItemTypeDto ItemType { get; set; }

    [Required]
    [Range(1, 100)] //Set default 100
    public required decimal Price { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required DateOnly? PurchaseDate { get; set; }

    [Required]
    public required EmployeeDto? HolderEmployee { get; set; }
}

public class EmployeeDto
{
    [Required]
    public required long Id { get; set; }

    [MaxLength(128)]
    [Required]
    public required string FullName { get; set; }
}
