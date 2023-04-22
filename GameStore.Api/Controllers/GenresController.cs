using System;
using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/genres")]
    public class GenresController : ApiController
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            return Json(await _genreService.GetAllAsync());
        }
    }
}
