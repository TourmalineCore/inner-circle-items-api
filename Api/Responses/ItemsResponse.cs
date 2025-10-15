namespace Api.Responses
{
    public class ItemsResponse
    {
        public required List<ItemDto> Items { get; set; }
    }

    public class ItemDto
    {
        public required long Id { get; set; }

        public required string Name { get; set; }

        public required string SerialNumber { get; set; }

        public required ItemTypeDto ItemType { get; set; }

        public required decimal Price { get; set; }

        public required string Description { get; set; }

        public required DateOnly? PurchaseDate { get; set; }

        public required EmployeeDto? HolderEmployee { get; set; }
    }

    public class EmployeeDto
    {
        public required long Id { get; set; }
    }
}
