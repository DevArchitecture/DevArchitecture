namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class CacheOptions
    {
        public string Host { get; set; }
        public string Password { get; set; }
        public int Database { get; set; }
        public int Port { get; set; }
    }
}