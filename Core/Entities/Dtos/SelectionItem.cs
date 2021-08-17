namespace Core.Entities.Dtos
{
    /// <summary>
    /// Simple selectable lists have been created to return with a single schema through the API.
    /// </summary>
    public class SelectionItem : IDto
    {
        public SelectionItem()
        {
        }


        public SelectionItem(dynamic id, string label)
        {
            Id = id;
            Label = label;
        }

        public dynamic Id { get; set; }
        public string ParentId { get; set; }
        public string Label { get; set; }
        public bool IsDisabled { get; set; }
    }
}