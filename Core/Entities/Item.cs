namespace Core.Entities;

public class Item : EntityBase
{
    // EntityFrameworkCore related empty default constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Item()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    public string Name { get; set; }

    public string SerialNumber { get; set; }

    public string Test { get; set; }

    public long ItemTypeId { get; set; }
    public ItemType ItemType { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public long? HolderEmployeeId { get; set; }
}
