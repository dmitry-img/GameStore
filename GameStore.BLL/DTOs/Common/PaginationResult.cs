using System.Collections.Generic;

namespace GameStore.BLL.DTOs.Common
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }

}
