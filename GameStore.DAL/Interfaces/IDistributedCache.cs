using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IDistributedCache<T>
    {
        Task<T> GetAsync(string key);

        Task SetAsync(string key, T value);

        Task RemoveAsync(string key);
    }
}
