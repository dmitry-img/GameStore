using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.DTOs.Common
{
    public class PaginationResult<T>
    {
        public PaginationResult(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalItems = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
        }

        public IEnumerable<T> Items { get; set; }

        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public static PaginationResult<T> ToPaginationResult(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return new PaginationResult<T>(items, count, pageNumber, pageSize);
        }
    }
}
