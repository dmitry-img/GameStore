using GameStore.BLL.DTOs;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameStore.Api.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IEnumerable<GameDTO> GetAll()
        {
            return _gameService.GetAll();
        }

        [HttpGet]
        public async Task<GameDTO> GetByKey(Guid key)
        {
            return await _gameService.GetByKeyAsync(key);
        }

        [HttpPost]
        public async Task Create([FromBody] GameDTO gameDTO)
        {
            await _gameService.CreateAsync(gameDTO);
        }

        [HttpPut]
        public async Task Update(Guid key, [FromBody] GameDTO gameDTO)
        {
            if(key != gameDTO.Key)
            {
                //TODO handle
            }

            await _gameService.UpdateAsync(gameDTO);
        }

        [HttpDelete]
        public async Task Delete(Guid key)
        {
            var game = await _gameService.GetByKeyAsync(key);

            await _gameService.DeleteAsync(game.Id);
        }

        [HttpGet]
        public void Download(Guid key)
        {

        }
    }
}