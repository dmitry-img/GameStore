using AutoMapper;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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

            _unitOfWork.Games.Create(game);

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UpdateGameDTO gameDTO)
        {
            var game = await _unitOfWork.Games.GetByKeyWithDetailsAsync(gameDTO.Key);

            _mapper.Map(gameDTO, game);

            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid key)
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
            var games = _unitOfWork.GameGenre.GetGamesByGenre(genreId);
            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public IEnumerable<GetGameDTO> GetAllByPlatformType(int platformTypeId)
        {
            var games = _unitOfWork.GamePlatformType.GetGamesByPlatformType(platformTypeId);
            var gameDTOs = _mapper.Map<IEnumerable<GetGameDTO>>(games);

            return gameDTOs;
        }

        public async Task<GetGameDTO> GetByKeyAsync(Guid key)
        {
            var game = await _unitOfWork.Games.GetByKeyAsync(key);
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
