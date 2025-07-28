namespace Core.Entities
{
    public class ItemType
    {
        public ItemType() 
        {
        }

        public long Id { get; set; }
        
        public long TenantId { get; set; }

        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }
}
