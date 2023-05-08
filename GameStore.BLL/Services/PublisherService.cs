using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task CreateAsync(CreatePublisherDTO publisherDTO)
        {
            var publisher = _mapper.Map<Publisher>(publisherDTO);

            _unitOfWork.Publishers.Create(publisher);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Publisher({publisher.Id}) was created!");
        }

        public async Task<IEnumerable<GetPublisherBriefDTO>> GetAllBriefAsync()
        {
            var publishers = await _unitOfWork.Publishers.GetAllAsync();

            return _mapper.Map<List<GetPublisherBriefDTO>>(publishers);
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
    }
}
