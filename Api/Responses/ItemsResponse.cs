namespace Api.Responses
{
    public class ItemsResponse
    {
        public List<ItemDto> Items { get; set; }
    }

    public class ItemDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public ItemTypeDto ItemType { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateOnly? PurchaseDate { get; set; }

        public EmployeeDto? HolderEmployee { get; set; }
    }

    public class ItemTypeDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class EmployeeDto
    {
        public long Id { get; set; }
    }
}
