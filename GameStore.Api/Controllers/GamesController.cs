using GameStore.Api.Models;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Interfaces;
using System;
using System.IO;
using System.Net.Http;
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
            return Json(_gameService.GetAll());
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByKey(Guid key)
        {
            var games = await _gameService.GetByKeyAsync(key);

            return Json(games);
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
            await _gameService.UpdateAsync(key, gameDTO);

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid key)
        {
            await _gameService.DeleteAsync(key);
            
            return Ok();
        }

        [HttpGet]
        [Route("api/Games/Download")]
        public HttpResponseMessage Download()
        {
            string appDataPath = ApplicationVariables.AppDataPath;
            string path = Path.Combine(appDataPath, "game.bin");
            
            return _gameService.Download(path);
        }
    }
}