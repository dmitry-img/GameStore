using System.Linq;

namespace GameStore.BLL.Interfaces
{
    public interface ISortStrategy<T>
    {
        IQueryable<T> Sort(IQueryable<T> query);
    }
}
