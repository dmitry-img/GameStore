using GameStore.Api.Models;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/games")]
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
        [Route("{key}")]
        public async Task<IHttpActionResult> GetByKey(string key)
        {
            var games = await _gameService.GetByKeyAsync(key);

            return Json(games);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody] CreateGameDTO gameDTO)
        {
            await _gameService.CreateAsync(gameDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{key}")]
        public async Task<IHttpActionResult> Update(string key, [FromBody] UpdateGameDTO gameDTO)
        {
            await _gameService.UpdateAsync(key, gameDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{key}")]
        public async Task<IHttpActionResult> Delete(string key)
        {
            await _gameService.DeleteAsync(key);
            
            return Ok();
        }

        [HttpGet]
        [Route("download/{key}")]
        public async Task<HttpResponseMessage> Download(string key)
        {
            var gameData = await _gameService.GetGameFileAsync(key);
            var game = await _gameService.GetByKeyAsync(key);

            string fileName = $"{game.Name}.bin";

            HttpResponseMessage result = 
                new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(gameData);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = 
                new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName;
            return result;
        }
    }
}