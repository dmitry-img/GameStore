using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateUserDTO createUserDTO)
        {
            var newUser = _mapper.Map<User>(createUserDTO);

            _unitOfWork.Users.Create(newUser);

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(string objectId)
        {
            var user = await _unitOfWork.Users.GetQuery().FirstOrDefaultAsync(u => u.ObjectId == objectId);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), objectId);
            }

            _unitOfWork.Users.Delete(user.Id);

            await _unitOfWork.SaveAsync();
        }

        public async Task<PaginationResult<GetUserDTO>> GetAllWithPaginationAsync(PaginationDTO paginationDTO)
        {
            var users = await _unitOfWork.Users
                .GetQuery()
                .Include(u => u.Role)
                .Where(u => !u.IsDeleted)
                .ToListAsync();

            return PaginationResult<GetUserDTO>.ToPaginationResult(
                _mapper.Map<IEnumerable<GetUserDTO>>(users),
                paginationDTO.PageNumber,
                paginationDTO.PageSize);
        }

        public async Task<GetUserDTO> GetByIdAsync(string objectId)
        {
            var user = await _unitOfWork.Users.GetQuery().FirstOrDefaultAsync(u => u.ObjectId == objectId);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), objectId);
            }

            return _mapper.Map<GetUserDTO>(user);
        }

        public async Task UpdateAsync(string objectId, UpdateUserDTO updateUserDTO)
        {
            var user = await _unitOfWork.Users.GetQuery().FirstOrDefaultAsync(u => u.ObjectId == objectId);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), objectId);
            }

            _mapper.Map(updateUserDTO, user);

            await _unitOfWork.SaveAsync();
        }
    }
}
