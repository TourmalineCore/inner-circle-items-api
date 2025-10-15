namespace Core.Entities
{
    public class Item : EntityBase
    {
        public Item()
        {
        }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public long ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateOnly? PurchaseDate { get; set; }

        public long? HolderEmployeeId { get; set; }
    }
}
