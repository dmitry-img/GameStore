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

        public PlatformTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPlatformTypeDTO>> GetAllAsync()
        {
            var platformTypes = await _unitOfWork.PlatformTypes.GetAllAsync();

            return _mapper.Map<IEnumerable<GetPlatformTypeDTO>>(platformTypes);
        }
    }
}
