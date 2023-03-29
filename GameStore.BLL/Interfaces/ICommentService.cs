using GameStore.BLL.DTOs;
using System.Collections;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        void CreateComment(int gameId, CommentDTO commentDTO);
        IEnumerable<CommentDTO> GetAllByGameKey(string key);
    }
}
