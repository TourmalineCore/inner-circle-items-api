namespace Core.Entities
{
    public class BrokenItemRecord
    {
        public BrokenItemRecord() 
        {

        }
        public long Id { get; set; }

        public long ItemId { get; set; }
        public Item Item { get; set; }

        public string Description { get; set; }

        public DateOnly DateOfBroke { get; set; }

        public DateOnly RepairDate {  get; set; }

        public bool isFinished { get; set; } = false;
    }
}
