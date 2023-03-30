using GameStore.BLL.DTOs;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
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
        public async Task Create([FromBody] CommentDTO commentDTO)
        {
            await _commentService.CreateAsync(commentDTO);
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDTO>> GetAllByGame([FromUri] Guid key)
        {
            return await _commentService.GetAllByGameKeyAsync(key);
        }
    }
}