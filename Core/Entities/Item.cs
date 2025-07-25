namespace Core.Entities
{
    public class Item
    {

        public Item()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string? SerialNumber { get; set; }

        public long ItemTypeId { get; set; }

        public double Price { get; set; }

        public DateOnly PurchaseDate { get; set; }

        public string? Description { get; set; }

        public long? HolderId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool isRemoved { get; set; } = false;
        public List<BrokenItemRecord> BrokenItemRecords { get; set; }

        public Status Status { get; set; } = Status.ReadyToUse;

    }
}
