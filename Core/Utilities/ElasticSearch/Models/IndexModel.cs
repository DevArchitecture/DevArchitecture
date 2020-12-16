namespace Core.Utilities.ElasticSearch.Models
{
    public class IndexModel
    {
        public string IndexName { get; set; }
        public string AliasName { get; set; }
        public int NumberOfReplicas { get; set; }
        public int NumberOfShards { get; set; }

        public IndexModel()
        {
            NumberOfReplicas = 1;

            NumberOfShards = 3;
        }
    }
}
