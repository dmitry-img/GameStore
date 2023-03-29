using GameStore.BLL.DTOs;
using GameStore.BLL.Interfaces;
using System.Collections.Generic;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        public void Create(GameDTO game)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GameDTO> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GameDTO> GetAllByGenre(int genreId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<GameDTO> GetAllByPlatformType(int platformTypeId)
        {
            throw new System.NotImplementedException();
        }

        public GameDTO GetGyKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, GameDTO game)
        {
            throw new System.NotImplementedException();
        }
    }
}
