using AutoMapper;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var game = await _unitOfWork.Games.GetByKeyWithDetailsAsync(key);

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

        public IEnumerable<GetGameDTO> GetAll()
        {
            var games = _unitOfWork.Games.GetAll();
            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public IEnumerable<GetGameDTO> GetAllByGenre(int genreId)
        {
            var games = _unitOfWork.Genres.Get(genreId).Games;
            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public IEnumerable<GetGameDTO> GetAllByPlatformType(int platformTypeId)
        {
            var games = _unitOfWork.PlatformTypes.Get(platformTypeId).Games;
            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public async Task<GetGameDTO> GetByKeyAsync(string key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);
            var gameDTO = _mapper.Map<GetGameDTO>(game);

            return gameDTO;
        }

        public async Task<GetGameDTO> GetByKeyWithDetailsAsync(string key)
        {
            var game = await _unitOfWork.Games.GetByKeyWithDetailsAsync(key);
            var gameDTO = _mapper.Map<GetGameDTO>(game);

            return gameDTO;
        }

        public HttpResponseMessage Download(string path)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
    }
}
