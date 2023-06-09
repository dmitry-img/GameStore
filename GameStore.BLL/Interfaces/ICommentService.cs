﻿using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Comment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        Task CreateAsync(string userObjectId, CreateCommentDTO commentDTO);

        Task<IEnumerable<GetCommentDTO>> GetAllByGameKeyAsync(string key);

        Task DeleteAsync(int id);
    }
}
