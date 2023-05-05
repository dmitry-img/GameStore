using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper, ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreateGameDTO gameDTO)
        {
            var game = _mapper.Map<Game>(gameDTO);

            game.Genres = (await _unitOfWork.Genres
                .FilterAsync(g => gameDTO.GenreIds.Contains(g.Id))).ToList();
            game.PlatformTypes = (await _unitOfWork.PlatformTypes
                .FilterAsync(pt => gameDTO.PlatformTypeIds.Contains(pt.Id))).ToList();

            _unitOfWork.Games.Create(game);
            await _unitOfWork.SaveAsync();

            _logger.Info($"Game({game.Id}) was created!");
        }

        public async Task UpdateAsync(string key, UpdateGameDTO gameDTO)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);

            if (game == null)
            {
                throw new NotFoundException(nameof(game), key);
            }

            _mapper.Map(gameDTO, game);

            game.Genres = (await _unitOfWork.Genres
               .FilterAsync(g => gameDTO.GenreIds.Contains(g.Id))).ToList();
            game.PlatformTypes = (await _unitOfWork.PlatformTypes
                .FilterAsync(pt => gameDTO.PlatformTypeIds.Contains(pt.Id))).ToList();

            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveAsync();

            _logger.Info($"Game({game.Id}) was updated!");
        }

        public async Task DeleteAsync(string key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);

            if (game == null)
            {
                throw new NotFoundException(nameof(game), key);
            }

            _unitOfWork.Games.Delete(game.Id);
            await _unitOfWork.SaveAsync();

            _logger.Info($"Game({game.Id}) was deleted!");
        }

        public async Task<IEnumerable<GetGameBriefDTO>> GetAllAsync()
        {
            var games = await _unitOfWork.Games
                .GetQuery()
                .Where(g => !g.IsDeleted)
                .ToListAsync();

            var gameDTOs = _mapper.Map<IEnumerable<GetGameBriefDTO>>(games);

            return gameDTOs;
        }

        public async Task<IEnumerable<GetGameBriefDTO>> GetAllByGenreAsync(int genreId)
        {
            var genre = await _unitOfWork.Genres.GetAsync(genreId);

            if (genre == null)
            {
                throw new NotFoundException(nameof(genre), genreId);
            }

            var games = await _unitOfWork.Games.GetGamesByGenreAsync(genreId);

            var gameDTOs = _mapper.Map<IEnumerable<GetGameBriefDTO>>(games);

            return gameDTOs;
        }

        public async Task<IEnumerable<GetGameBriefDTO>> GetAllByPlatformTypeAsync(int platformTypeId)
        {
            var platformType = await _unitOfWork.PlatformTypes.GetAsync(platformTypeId);

            if (platformType == null)
            {
                throw new NotFoundException(nameof(platformType), platformTypeId);
            }

            var games = await _unitOfWork.Games.GetGamesByPlatformTypeAsync(platformTypeId);

            var gameDTOs = _mapper.Map<IEnumerable<GetGameBriefDTO>>(games);

            return gameDTOs;
        }

        public async Task<GetGameDTO> GetByKeyAsync(string key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);

            if (game == null)
            {
                throw new NotFoundException(nameof(game), key);
            }

            var gameDTO = _mapper.Map<GetGameDTO>(game);

            return gameDTO;
        }

        public async Task<MemoryStream> GetGameFileAsync(string gameKey)
        {
            var game = await _unitOfWork.Games
                .GetQuery()
                .FirstOrDefaultAsync(g => g.Key == gameKey);

            if (game == null)
            {
                throw new NotFoundException(nameof(game), gameKey);
            }

            _logger.Info($"Game({game.Id}) was downloaded!");

            return new MemoryStream(Encoding.ASCII.GetBytes(game.Name));
        }

        public int GetCount()
        {
            return _unitOfWork.Games
                .GetQuery()
                .Where(g => !g.IsDeleted)
                .Count();
        }
    }
}
