using GameStore.BLL.DTOs;
using System.Collections;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        void Create(GameDTO game);
        void Update(int id, GameDTO game);
        void Delete(int id);
        GameDTO GetGyKey(string key);
        IEnumerable<GameDTO> GetAll();
        IEnumerable<GameDTO> GetAllByGenre(int genreId);
        IEnumerable<GameDTO> GetAllByPlatformType(int platformTypeId);

    }
}
