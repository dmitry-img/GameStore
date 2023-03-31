using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Interfaces;
using System;
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
        public IHttpActionResult GetAll()
        {
            return Ok(_gameService.GetAll());
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByKey(Guid key)
        {
            var games = await _gameService.GetByKeyAsync(key);

            return Ok(games);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] CreateGameDTO gameDTO)
        {
            await _gameService.CreateAsync(gameDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid key, [FromBody] UpdateGameDTO gameDTO)
        {
            if(key != gameDTO.Key)
            {
                return BadRequest();
            }

            await _gameService.UpdateAsync(gameDTO);

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid key)
        {
            await _gameService.DeleteAsync(key);
            
            return Ok();
        }

        [HttpGet]
        public void Download(Guid key)
        {

        }
    }
}