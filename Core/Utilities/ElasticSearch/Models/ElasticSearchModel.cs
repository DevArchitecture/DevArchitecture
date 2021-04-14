namespace Core.Utilities.ElasticSearch.Models
{
    using Nest;

    public class ElasticSearchModel
    {
        public Id ElasticId { get; set; }
        public string IndexName { get; set; }
    }
}
