using FluentValidation;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Validators;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/comments")]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;
        private readonly IValidator<CreateCommentDTO> _createCommentValidator;

        public CommentsController(ICommentService commentService, IValidator<CreateCommentDTO> createCommentValidator)
        {
            _commentService = commentService;
            _createCommentValidator = createCommentValidator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody] CreateCommentDTO commentDTO)
        {
            var result = _createCommentValidator.Validate(commentDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(string.Join(", ", errors));
            }

            await _commentService.CreateAsync(commentDTO);

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
