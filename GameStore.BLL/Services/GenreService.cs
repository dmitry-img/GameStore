using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateGenreDTO createGenreDTO)
        {
            var newGenre = _mapper.Map<Genre>(createGenreDTO);

            _unitOfWork.Genres.Create(newGenre);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Genres.Delete(id);

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GetGenreDTO>> GetAll()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();

            return _mapper.Map<IEnumerable<GetGenreDTO>>(genres);
        }

        public async Task<PaginationResult<GetGenreDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO)
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();

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

            _mapper.Map(updateGenreDTO, genre);

            await _unitOfWork.SaveAsync();
        }
    }
}
