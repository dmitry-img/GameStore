using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        Task CreateAsync(CreateGameDTO gameDTO);
        Task UpdateAsync(Guid key, UpdateGameDTO gameDTO);
        Task DeleteAsync(Guid key);
        Task<GetGameDTO> GetByKeyAsync(Guid key);
        Task<GetGameDTO> GetByKeyWithDetailsAsync(Guid key);
        IEnumerable<GetGameDTO> GetAll();
        IEnumerable<GetGameDTO> GetAllByGenre(int genreId);
        IEnumerable<GetGameDTO> GetAllByPlatformType(int platformTypeId);
        HttpResponseMessage Download(string path);
    }
}
