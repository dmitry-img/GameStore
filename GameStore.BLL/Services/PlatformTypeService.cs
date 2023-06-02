using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlatformTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreatePlatformTypeDTO createPlatformTypeDTO)
        {
            var newPlatformType = _mapper.Map<PlatformType>(createPlatformTypeDTO);

            _unitOfWork.PlatformTypes.Create(newPlatformType);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.PlatformTypes.Delete(id);

            await _unitOfWork.SaveAsync();
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
