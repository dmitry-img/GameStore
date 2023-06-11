using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Role;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public RoleService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreateRoleDTO createRoleDTO)
        {
            var role = await _unitOfWork.Roles.GetQuery().FirstOrDefaultAsync(r => r.Name == createRoleDTO.Name);

            if (role != null)
            {
                if (role.IsDeleted)
                {
                    role.IsDeleted = false;
                    role.DeletedAt = null;
                }
                else
                {
                    throw new BadRequestException("The role already exists!");
                }
            }
            else
            {
                role = _mapper.Map<Role>(createRoleDTO);

                _unitOfWork.Roles.Create(role);
            }

            await _unitOfWork.SaveAsync();

            _logger.Info($"Role({role.Id}) has been created!");
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetAsync(id);

            if (role == null)
            {
                throw new NotFoundException(nameof(role), id);
            }

            _unitOfWork.Roles.Delete(id);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Role({role.Id}) has been deleted!");
        }

        public async Task<IEnumerable<GetRoleDTO>> GetAllAsync()
        {
            var roles = await _unitOfWork.Roles
                .GetQuery()
                .Where(r => !r.IsDeleted)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetRoleDTO>>(roles);
        }

        public async Task<PaginationResult<GetRoleDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO)
        {
            var roles = await _unitOfWork.Roles
                .GetQuery()
                .Where(r => !r.IsDeleted)
                .ToListAsync();

            return PaginationResult<GetRoleDTO>.ToPaginationResult(
                _mapper.Map<IEnumerable<GetRoleDTO>>(roles),
                paginationDTO.PageNumber,
                paginationDTO.PageSize);
        }

        public async Task<GetRoleDTO> GetByIdAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetAsync(id);

            return _mapper.Map<GetRoleDTO>(role);
        }
    }
}
