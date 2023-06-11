using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.Role;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper, ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreateGenreDTO createGenreDTO)
        {
            var genre = await _unitOfWork.Genres.GetQuery().FirstOrDefaultAsync(r => r.Name == createGenreDTO.Name);

            if (genre != null)
            {
                if (genre.IsDeleted)
                {
                    genre.IsDeleted = false;
                    genre.DeletedAt = null;
                }
                else
                {
                    throw new BadRequestException("The genre already exists!");
                }
            }
            else
            {
                genre = _mapper.Map<Genre>(createGenreDTO);

                _unitOfWork.Genres.Create(genre);
            }

            await _unitOfWork.SaveAsync();

            _logger.Info($"Genre({genre.Id}) has been created!");
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _unitOfWork.Genres
                .GetQuery()
                .Include(g => g.ChildGenres)
                .Include(g => g.Games)
                .FirstOrDefaultAsync(g => g.Id == id);

            genre.ChildGenres.Clear();
            genre.Games.Clear();

            _unitOfWork.Genres.Delete(id);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Genre({genre.Id}) has been deleted!");
        }

        public async Task<IEnumerable<GetGenreDTO>> GetAllAsync()
        {
            var genres = await _unitOfWork.Genres
                .GetQuery()
                .Where(g => !g.IsDeleted)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetGenreDTO>>(genres);
        }

        public async Task<PaginationResult<GetGenreDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO)
        {
            var genres = await _unitOfWork.Genres
                .GetQuery()
                .Where(g => !g.IsDeleted)
                .ToListAsync();

            return PaginationResult<GetGenreDTO>.ToPaginationResult(
                _mapper.Map<IEnumerable<GetGenreDTO>>(genres),
                paginationDTO.PageNumber,
                paginationDTO.PageSize);
        }

        public async Task UpdateAsync(int id, UpdateGenreDTO updateGenreDTO)
        {
            var genre = await _unitOfWork.Genres.GetAsync(id);

            if (genre == null)
            {
                throw new NotFoundException(nameof(genre), id);
            }

            var oldParentGenreId = genre.ParentGenreId;

            _mapper.Map(updateGenreDTO, genre);

            if (oldParentGenreId.HasValue && genre.ParentGenreId.HasValue)
            {
                await ReplaceParentGenreInGamesWithGenre(genre, oldParentGenreId.Value);
            }
            else if (!oldParentGenreId.HasValue && genre.ParentGenreId.HasValue)
            {
                await AddParentGenreToGamesWithGenre(genre);
            }
            else if (oldParentGenreId.HasValue && !genre.ParentGenreId.HasValue)
            {
                RemoveParentGenreFromGamesWithGenre(genre, oldParentGenreId.Value);
            }

            await _unitOfWork.SaveAsync();

            _logger.Info($"Genre({genre.Id}) has been updated!");
        }

        private async Task ReplaceParentGenreInGamesWithGenre(Genre genre, int oldParentGenreId)
        {
            var gamesQuery = _unitOfWork.Games.GetQuery().Include(g => g.Genres);

            var gamesWithOldParentGenreId = gamesQuery.Where(g => g.Genres.Any(gen => gen.Id == oldParentGenreId));

            foreach (var game in gamesWithOldParentGenreId)
            {
                var childGenresOfOldParent = game.Genres.Where(g => g.ParentGenreId == oldParentGenreId);

                if (!childGenresOfOldParent.Any())
                {
                    var oldParentGenre = game.Genres.FirstOrDefault(g => g.Id == oldParentGenreId);
                    if (oldParentGenre != null)
                    {
                        game.Genres.Remove(oldParentGenre);
                    }
                }

                if (genre.ParentGenreId.HasValue)
                {
                    var newParentGenre = await _unitOfWork.Genres.GetAsync(genre.ParentGenreId.Value);
                    game.Genres.Add(newParentGenre);
                }
            }
        }

        private async Task AddParentGenreToGamesWithGenre(Genre genre)
        {
            var gamesQuery = _unitOfWork.Games.GetQuery().Include(g => g.Genres);

            var gamesWithGenreId = gamesQuery.Where(g => g.Genres.Any(gen => gen.Id == genre.Id));

            foreach (var game in gamesWithGenreId)
            {
                if (genre.ParentGenreId.HasValue)
                {
                    var parentGenre = await _unitOfWork.Genres.GetAsync(genre.ParentGenreId.Value);
                    game.Genres.Add(parentGenre);
                }
            }
        }

        private void RemoveParentGenreFromGamesWithGenre(Genre genre, int oldParentGenreId)
        {
            var gamesQuery = _unitOfWork.Games.GetQuery().Include(g => g.Genres);

            var gamesWithOldParentGenreId = gamesQuery.Where(g => g.Genres.Any(gen => gen.Id == oldParentGenreId));

            foreach (var game in gamesWithOldParentGenreId)
            {
                var childGenresOfOldParent = game.Genres.Where(g => g.ParentGenreId == oldParentGenreId);

                if (!childGenresOfOldParent.Any())
                {
                    var oldParentGenre = game.Genres.FirstOrDefault(g => g.Id == oldParentGenreId);
                    if (oldParentGenre != null)
                    {
                        game.Genres.Remove(oldParentGenre);
                    }
                }
            }
        }
    }
}
