namespace Api.Responses
{
    public class ItemsListResponse
    {
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public ItemType ItemType { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateOnly? PurchaseDate { get; set; }

        public Employee? HolderEmployee { get; set; }
    }

    public class ItemType
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class Employee
    {
        public long Id { get; set; }
    }
}
