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

namespace GameStore.BLL.Services
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateRoleDTO createRoleDTO)
        {
            var role = await _unitOfWork.Roles.GetQuery().FirstOrDefaultAsync(r => r.Name == createRoleDTO.Name);

            if (role != null)
            {
                throw new BadRequestException("The role already exists!");
            }

            var newRole = _mapper.Map<Role>(createRoleDTO);

            _unitOfWork.Roles.Create(newRole);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Roles.Delete(id);

            await _unitOfWork.SaveAsync();
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

        public async Task UpdateAsync(int id, UpdateRoleDTO updateRoleDTO)
        {
            var role = await _unitOfWork.Roles.GetQuery().FirstOrDefaultAsync(r => r.Name == updateRoleDTO.Name);

            if (role != null)
            {
                throw new BadRequestException("The role already exists!");
            }

            var roleToUpdate = await _unitOfWork.Roles.GetAsync(id);

            _mapper.Map(updateRoleDTO, roleToUpdate);

            await _unitOfWork.SaveAsync();
        }
    }
}
