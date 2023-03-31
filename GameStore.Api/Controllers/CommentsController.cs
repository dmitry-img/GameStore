using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace GameStore.Api.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] CreateCommentDTO commentDTO)
        {
            await _commentService.CreateAsync(commentDTO);

            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllByGame([FromUri] Guid key)
        {
            var comments = await _commentService.GetAllByGameKeyAsync(key);

            return Ok(comments);
        }
    }
}