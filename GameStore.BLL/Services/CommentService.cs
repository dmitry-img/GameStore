using AutoMapper;
using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<GetCommentDTO>> GetAllByGameKeyAsync(Guid key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);

            if (game == null)
                throw new NotFoundException(nameof(game), key);

            var parentComments = game.Comments.Where(c => c.ParentCommentId == null);

            var comments = _mapper.Map<IEnumerable<GetCommentDTO>>(parentComments);

            return comments;
        }
    }
}
