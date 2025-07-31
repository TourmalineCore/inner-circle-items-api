using Core.Entities;

namespace Api.Responses
{
    public class ItemsListResponse
    {
        public List<ItemsListItem> Items { get; set; }
    }

    public class Employee
    {
        public long? Id { get; set; }
    }
    public class ItemsListItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? SerialNumber { get; set; }

        // ItemTypeListItem is used to avoid cyclic dependency between item and item type
        public ItemTypeListItem ItemType { get; set; }

        public decimal Price { get; set; }

        public DateOnly? PurchaseDate {  get; set; } 

        public Employee? HolderEmployee { get; set; }
    }
}
