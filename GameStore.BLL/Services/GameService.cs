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
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(GameDTO gameDTO)
        {
            var game = _mapper.Map<Game>(gameDTO);

            _unitOfWork.Games.Create(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(GameDTO gameDTO)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(gameDTO.Key);

            _mapper.Map(gameDTO, game);

            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var game = _unitOfWork.Games.Get(id);
            if (game == null)
                throw new NotFoundException(nameof(game), id);

            _unitOfWork.Games.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<GameDTO> GetAll()
        {
            var games = _unitOfWork.Games.GetAll();
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);

            return gameDTOs;
        }

        public IEnumerable<GameDTO> GetAllByGenre(int genreId)
        {
            var games = _unitOfWork.GameGenre.GetGamesByGenre(genreId);
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);

            return gameDTOs;
        }

        public IEnumerable<GameDTO> GetAllByPlatformType(int platformTypeId)
        {
            var games = _unitOfWork.GamePlatformType.GetGamesByPlatformType(platformTypeId);
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);

            return gameDTOs;
        }

        public async Task<GameDTO> GetByKeyAsync(Guid key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);
            var gameDTO = _mapper.Map<GameDTO>(game);

            return gameDTO;
        }
    }
}
