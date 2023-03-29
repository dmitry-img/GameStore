using AutoMapper;
using GameStore.BLL.DTOs;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;

namespace GameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateComment(int gameId, CommentDTO commentDTO)
        {
            var game = _unitOfWork.Games.Get(gameId);
            if(game == null)
            {
                //TODO handle if game is null
            }

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();
            var comment = mapper.Map<CommentDTO, Comment> (commentDTO);

            game.Comments.Add(comment);
        }

        public IEnumerable<CommentDTO> GetAllByGameKey(string key)
        {
            var game = _unitOfWork.Games.GetByKey(key);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comment>()).CreateMapper();
            var comments = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(game.Comments);

            return comments;
        }
    }
}
