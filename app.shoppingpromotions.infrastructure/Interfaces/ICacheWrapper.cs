namespace app.shoppingpromotions.infrastructure.Interfaces
{
    public interface ICacheWrapper
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiration);
        bool Remove(string key);
    }
}