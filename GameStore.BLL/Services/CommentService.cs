using AutoMapper;
using GameStore.BLL.DTOs;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task CreateAsync(CommentDTO commentDTO)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(commentDTO.GameKey);

            if (game == null)
                throw new NotFoundException(nameof(game), commentDTO.GameKey);
            
            var comment = _mapper.Map<Comment> (commentDTO);

            game.Comments.Add(comment);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CommentDTO>> GetAllByGameKeyAsync(Guid key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);

            if (game == null)
                throw new NotFoundException(nameof(game), key);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();
            var comments = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(game.Comments);

            return comments;
        }
    }
}
