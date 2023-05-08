using System.Collections.Generic;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Genre;

namespace GameStore.BLL.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GetGenreDTO>> GetAllAsync();
    }
}
