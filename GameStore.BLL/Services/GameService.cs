﻿using AutoMapper;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text;

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

        public async Task CreateAsync(CreateGameDTO gameDTO)
        {
            var game = _mapper.Map<Game>(gameDTO);

            game.Genres = _unitOfWork.Genres
                .Filter(g => gameDTO.GenreIds.Contains(g.Id)).ToList();
            game.PlatformTypes = _unitOfWork.PlatformTypes
                .Filter(pt => gameDTO.PlatformTypeIds.Contains(pt.Id)).ToList();

            _unitOfWork.Games.Create(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(string key, UpdateGameDTO gameDTO)
        {
            var game = await _unitOfWork.Games
                .GetQuery()
                .Include(g => g.Genres)
                .Include(g => g.PlatformTypes)
                .FirstOrDefaultAsync(g => g.Key == key && !g.IsDeleted); ;

            if (game == null)
                throw new NotFoundException(nameof(game), key);

            _mapper.Map(gameDTO, game);

            game.Genres = _unitOfWork.Genres
               .Filter(g => gameDTO.GenreIds.Contains(g.Id)).ToList();
            game.PlatformTypes = _unitOfWork.PlatformTypes
                .Filter(pt => gameDTO.PlatformTypeIds.Contains(pt.Id)).ToList();

            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(string key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);
            if (game == null)
                throw new NotFoundException(nameof(game), key);

            _unitOfWork.Games.Delete(game.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GetGameDTO>> GetAllAsync()
        {
            var gamesQuery = _unitOfWork.Games
                .GetQuery()
                .Include(g => g.Genres)
                .Include(g => g.PlatformTypes)
                .Where(g => !g.IsDeleted);
                
            await gamesQuery.ForEachAsync(game => 
                game.Genres = game.Genres.Where(genre => 
                    genre.ParentGenreId == null).ToList());

            var games = await gamesQuery.ToListAsync();

            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public IEnumerable<GetGameDTO> GetAllByGenre(int genreId)
        {
            var games = _unitOfWork.Games.GetGamesByGenre(genreId);
            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public IEnumerable<GetGameDTO> GetAllByPlatformType(int platformTypeId)
        {
            var games = _unitOfWork.Games.GetGamesByPlatformType(platformTypeId);
            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public async Task<GetGameDTO> GetByKeyAsync(string key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);
                
            if (game == null)
                throw new NotFoundException(nameof(game), key);

            var gameDTO = _mapper.Map<GetGameDTO>(game);

            return gameDTO;
        }

        public async Task<MemoryStream> GetGameFileAsync(string gameKey)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(gameKey);

            if (game == null)
                throw new NotFoundException(nameof(game), gameKey);

            return new MemoryStream(Encoding.ASCII.GetBytes(game.Name));
        }
    }
}
