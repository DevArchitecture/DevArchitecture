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

		public SelectionItem(int id, string label) : this(id.ToString(), label)
		{
		}

		public SelectionItem(string id, string label)
		{
			this.Id = id;
			this.Label = label;
		}

		public string Id { get; set; }
		public string ParentId { get; set; }
		public string Label { get; set; }
		public bool IsDisabled { get; set; }
	}
}
