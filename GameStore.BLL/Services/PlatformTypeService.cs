using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public PlatformTypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreatePlatformTypeDTO createPlatformTypeDTO)
        {
            var newPlatformType = _mapper.Map<PlatformType>(createPlatformTypeDTO);

            _unitOfWork.PlatformTypes.Create(newPlatformType);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Platform type({newPlatformType.Id}) has been created!");
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.PlatformTypes.Delete(id);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Platform type({id}) has been deleted!");

        }

        public async Task<IEnumerable<GetPlatformTypeDTO>> GetAllAsync()
        {
            var platformTypes = await _unitOfWork.PlatformTypes.GetAllAsync();

            return _mapper.Map<IEnumerable<GetPlatformTypeDTO>>(platformTypes);
        }

        public async Task UpdateAsync(int id, UpdatePlatformTypeDTO updatePlatformTypeDTO)
        {
            var platformType = await _unitOfWork.PlatformTypes.GetAsync(id);

            if (platformType == null)
            {
                throw new NotFoundException(nameof(platformType), id);
            }

            _mapper.Map(updatePlatformTypeDTO, platformType);

            await _unitOfWork.SaveAsync();
        }
    }
}
