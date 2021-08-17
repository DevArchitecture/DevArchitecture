namespace Core.Utilities.ElasticSearch.Models
{
    public class SearchParameters
    {
        public string IndexName { get; set; }
        public int From { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}