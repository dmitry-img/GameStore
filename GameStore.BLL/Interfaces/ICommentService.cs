using GameStore.BLL.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        Task CreateAsync(CommentDTO commentDTO);
        Task<IEnumerable<CommentDTO>> GetAllByGameKeyAsync(Guid key);
    }
}
