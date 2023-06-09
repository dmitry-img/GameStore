using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/genres")]
    [Authorize(Roles = "Manager")]
    public class GenresController : ApiController
    {
        private readonly IGenreService _genreService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateGenreDTO> _createGenreValidator;
        private readonly IValidator<UpdateGenreDTO> _updateGenreValidator;

        public GenresController(
            IGenreService genreService,
            IValidationService validationService,
            IValidator<CreateGenreDTO> createGenreValidator,
            IValidator<UpdateGenreDTO> updateGenreValidator)
        {
            _genreService = genreService;
            _validationService = validationService;
            _createGenreValidator = createGenreValidator;
            _updateGenreValidator = updateGenreValidator;
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
            _validationService.Validate(createGenreDTO, _createGenreValidator);

            await _genreService.CreateAsync(createGenreDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdateGenreDTO updateGenreDTO)
        {
            _validationService.Validate(updateGenreDTO, _updateGenreValidator);

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
