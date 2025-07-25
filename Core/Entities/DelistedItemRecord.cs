namespace Core.Entities
{
    public class DelistedItemRecord
    {
        public DelistedItemRecord() 
        {

        }
        public long ItemId { get; set; }
        public Item Item { get; set; }
        public string Description { get; set; }
        public DateOnly Date {  get; set; }
    }
}
