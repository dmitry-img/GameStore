using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;
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
        [Route("list")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAll()
        {
            return Json(await _genreService.GetAll());
        }

        [HttpGet]
        [Route("paginated-list")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAllWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            return Json(await _genreService.GetAllWithPaginationAsync(paginationDTO));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateGenreDTO createGenreDTO)
        {
            await _genreService.CreateAsync(createGenreDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdateGenreDTO updateGenreDTO)
        {
            await _genreService.UpdateAsync(id, updateGenreDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);

            return Ok();
        }
    }
}
