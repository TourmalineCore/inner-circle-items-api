namespace Api.Responses
{
    public class ItemTypesListResponse
    {
        public List<ItemTypeListItem> ItemTypes { get; set; }
    }

    public class ItemTypeListItem
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
