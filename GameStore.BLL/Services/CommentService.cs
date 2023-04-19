using AutoMapper;
using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;
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
        private readonly ILog _logger;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreateCommentDTO commentDTO)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(commentDTO.GameKey);

            if (game == null)
            {
                throw new NotFoundException(nameof(game), commentDTO.GameKey);
            }

            var comment = _mapper.Map<Comment>(commentDTO);

            game.Comments.Add(comment);
            await _unitOfWork.SaveAsync();

            _logger.Info($"Comment({comment.Id}) was created for game({comment.GameId})");
        }

        public async Task<IEnumerable<GetCommentDTO>> GetAllByGameKeyAsync(string key)
        {
            var game = await _unitOfWork.Games
                .GetQuery()
                .Include(g => g.Comments)
                .FirstOrDefaultAsync(g => g.Key == key);

            if (game == null)
            {
                throw new NotFoundException(nameof(game), key);
            }

            var comments = _mapper.Map<IEnumerable<GetCommentDTO>>(game.Comments);

            return comments;
        }
    }
}
