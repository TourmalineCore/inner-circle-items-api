namespace Core.Entities
{
    public class Item
    {
        public Item ()
        {

        }

        public long Id { get; set; }

        public long TenantId { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; } = string.Empty;

        public long ItemTypeId { get; set; }

        public ItemType ItemType { get; set; }

        public decimal Price { get; set; }

        public DateOnly? PurchaseDate { get; set; }  

        public long? HolderId { get; set; }
    }
}
