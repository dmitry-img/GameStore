using FluentValidation;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Interfaces;
using GameStore.Shared;
using GameStore.Shared.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

using static GameStore.Shared.Infrastructure.Constants;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/comments")]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateCommentDTO> _createCommentValidator;

        public CommentsController(
            ICommentService commentService,
            IValidationService validationService,
            IValidator<CreateCommentDTO> createCommentValidator)
        {
            _commentService = commentService;
            _validationService = validationService;
            _createCommentValidator = createCommentValidator;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IHttpActionResult> Create([FromBody] CreateCommentDTO commentDTO)
        {
            _validationService.Validate(commentDTO, _createCommentValidator);

            await _commentService.CreateAsync(UserContext.UserObjectId, commentDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = ModeratorRoleName)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _commentService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet]
        [Route("{key}")]
        public async Task<IHttpActionResult> GetAllByGame(string key)
        {
            var comments = await _commentService.GetAllByGameKeyAsync(key);

            return Json(comments);
        }
    }
}
