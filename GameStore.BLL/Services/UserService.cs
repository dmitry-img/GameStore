using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.Auth;
using GameStore.BLL.DTOs.Ban;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Enums;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserValidationService _userValidationService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly Dictionary<BanDuration, DateTime> _banDurationDictionary = new Dictionary<BanDuration, DateTime>
        {
            { BanDuration.OneHour, DateTime.UtcNow.AddHours(1) },
            { BanDuration.OneDay, DateTime.UtcNow.AddDays(1) },
            { BanDuration.OneWeek, DateTime.UtcNow.AddDays(7) },
            { BanDuration.OneMonth, DateTime.UtcNow.AddMonths(1) },
            { BanDuration.Permanent, DateTime.MaxValue }
        };

        public UserService(
            IUnitOfWork unitOfWork,
            IUserValidationService userValidationService,
            IMapper mapper,
            ILog logger)
        {
            _unitOfWork = unitOfWork;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreateUserDTO createUserDTO)
        {
            _userValidationService.CheckUserExistsByEmail(createUserDTO.Email);
            _userValidationService.CheckUserExistsByUsername(createUserDTO.Username);

            var newUser = _mapper.Map<User>(createUserDTO);

            _unitOfWork.Users.Create(newUser);

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);

            await _unitOfWork.SaveAsync();

            _logger.Info($"User({newUser.Id}) has been created!");
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

            _logger.Info($"User({user.Id}) has been deleted!");
        }

        public async Task<PaginationResult<GetUserDTO>> GetAllWithPaginationAsync(string userObjectId, PaginationDTO paginationDTO)
        {
            var users = await _unitOfWork.Users
                .GetQuery()
                .Include(u => u.Role)
                .Where(u => !u.IsDeleted && u.ObjectId != userObjectId)
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
            var user = await _unitOfWork.Users
                .GetQuery()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.ObjectId == objectId);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), objectId);
            }

            if (user.Role?.Name == "Publisher")
            {
                var publisher = await _unitOfWork.Publishers
                    .GetQuery()
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);
                if (publisher != null)
                {
                    publisher.UserId = null;
                }
            }

            _mapper.Map(updateUserDTO, user);

            await _unitOfWork.SaveAsync();

            _logger.Info($"User({user.Id}) has been updated!");
        }

        public async Task BanAsync(BanDTO banDTO)
        {
            var comment = await _unitOfWork.Comments
                .GetQuery()
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == banDTO.CommentId);

            comment.User.BanEndDate = _banDurationDictionary[banDTO.BanDuration];

            await _unitOfWork.SaveAsync();

            _logger.Info($"User {comment.User.Username} " +
                $"has been banned until {_banDurationDictionary[banDTO.BanDuration]}");
        }

        public async Task<bool> IsBannedAsync(string userObjectId)
        {
            var user = await _unitOfWork.Users.GetQuery().FirstOrDefaultAsync(u => u.ObjectId == userObjectId);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), userObjectId);
            }

            if (user.BanEndDate == null)
            {
                return false;
            }

            if (user.BanEndDate < DateTime.UtcNow)
            {
                user.BanEndDate = null;

                await _unitOfWork.SaveAsync();

                return false;
            }

            return true;
        }
    }
}
