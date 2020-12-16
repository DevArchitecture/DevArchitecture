namespace Core.Entities.Dtos
{
	/// <summary>
	/// Basit seçilebilir listeleri, API üzerinden tek bir şemayla dönebilmek için
	/// yaratılmıştır.
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
