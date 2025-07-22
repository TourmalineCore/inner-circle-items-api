namespace Core.Entities
{
    public class ItemType
    {
        public ItemType(string name)
        {
            Name = name;
        }
        public ItemType() { }
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
