using FluentValidation;
using FluentValidation.Results;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Interfaces;
using System.Linq;
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
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateGameDTO> _createGameValidator;
        private readonly IValidator<UpdateGameDTO> _updateGameValidator;
        private readonly IValidator<FilterGameDTO> _filterGameValidator;

        public GamesController(
            IGameService gameService,
            IValidationService validationService,
            IValidator<CreateGameDTO> createGameValidator,
            IValidator<UpdateGameDTO> updateGameValidator,
            IValidator<FilterGameDTO> filterGameValidator)
        {
            _gameService = gameService;
            _validationService = validationService;
            _createGameValidator = createGameValidator;
            _updateGameValidator = updateGameValidator;
            _filterGameValidator = filterGameValidator;
        }

        [HttpGet]
        [Route("count")]
        public IHttpActionResult GetCount()
        {
            return Ok(_gameService.GetCount());
        }

        [HttpGet]
        [Route("list")]
        public async Task<IHttpActionResult> GetList([FromUri]FilterGameDTO filter)
        {
            _validationService.Validate(filter, _filterGameValidator);

            var games = await _gameService.GetFilteredAsync(filter);
            return Json(games);
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
            _validationService.Validate(gameDTO, _createGameValidator);

            await _gameService.CreateAsync(gameDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{key}")]
        public async Task<IHttpActionResult> Update(string key, [FromBody] UpdateGameDTO gameDTO)
        {
            _validationService.Validate(gameDTO, _updateGameValidator);

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
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(gameData)
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }
    }
}
