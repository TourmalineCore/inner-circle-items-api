namespace Core.Entities
{
    public class ItemType : EntityBase
    {
        public ItemType()
        {
        }
        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }
}
