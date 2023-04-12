using AutoMapper;
using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCommentDTO commentDTO)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(commentDTO.GameKey);

            if (game == null)
                throw new NotFoundException(nameof(game), commentDTO.GameKey);
            
            var comment = _mapper.Map<Comment> (commentDTO);

            game.Comments.Add(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GetCommentDTO>> GetAllByGameKeyAsync(string key)
        {
            var game = await _unitOfWork.Games
                .GetQuery()
                .Include(g => g.Comments)
                .FirstOrDefaultAsync(g => g.Key == key);

            if (game == null)
                throw new NotFoundException(nameof(game), key);

            var comments = _mapper.Map<IEnumerable<GetCommentDTO>>(game.Comments);

            return comments;
        }
    }
}
