namespace Core.CrossCuttingConcerns.Caching
{
    /// <summary>
    /// Tüm Cache Managerlar bu interface i kullanacaktır.
    /// </summary>
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object data, int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
