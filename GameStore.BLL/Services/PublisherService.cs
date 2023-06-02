using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper, ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreatePublisherDTO createPublisherDTO)
        {
            var publisher = _mapper.Map<Publisher>(createPublisherDTO);

            _unitOfWork.Publishers.Create(publisher);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Publisher({publisher.Id}) was created!");
        }

        public async Task DeleteAsync(int id)
        {
            var games = await _unitOfWork.Games
                .GetQuery()
                .Where(g => g.PublisherId == id)
                .ToListAsync();

            foreach (var game in games)
            {
                game.PublisherId = null;
            }

            _unitOfWork.Publishers.Delete(id);

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<GetPublisherBriefDTO>> GetAllBriefAsync()
        {
            var publishers = await _unitOfWork.Publishers
                .GetQuery()
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return _mapper.Map<List<GetPublisherBriefDTO>>(publishers);
        }

        public async Task<PaginationResult<GetPublisherBriefDTO>> GetAllBriefWithPaginationAsync(PaginationDTO paginationDTO)
        {
            var publishers = await _unitOfWork.Publishers
                .GetQuery()
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return PaginationResult<GetPublisherBriefDTO>.ToPaginationResult(
                _mapper.Map<List<GetPublisherBriefDTO>>(publishers),
                paginationDTO.PageNumber,
                paginationDTO.PageSize);
        }

        public async Task<GetPublisherDTO> GetByCompanyNameAsync(string companyName)
        {
            var publisher = await _unitOfWork.Publishers
                .GetQuery()
                .FirstOrDefaultAsync(p => p.CompanyName == companyName);

            if (publisher == null)
            {
                throw new NotFoundException(nameof(publisher), companyName);
            }

            return _mapper.Map<GetPublisherDTO>(publisher);
        }

        public async Task UpdateAsync(string companyName, UpdatePublisherDTO updatePublisherDTO)
        {
            var publisher = await _unitOfWork.Publishers
                .GetQuery()
                .FirstOrDefaultAsync(p => p.CompanyName == companyName);

            _mapper.Map(updatePublisherDTO, publisher);

            await _unitOfWork.SaveAsync();
        }
    }
}
