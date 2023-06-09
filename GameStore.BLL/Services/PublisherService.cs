﻿using System.Collections.Generic;
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

using static GameStore.Shared.Infrastructure.Constants;

namespace GameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public PublisherService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILog logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(CreatePublisherDTO createPublisherDTO)
        {
            var publisher = _mapper.Map<Publisher>(createPublisherDTO);

            var user = await _unitOfWork.Users.GetQuery().FirstOrDefaultAsync(u => u.Username == createPublisherDTO.Username);

            publisher.User = user;

            _unitOfWork.Publishers.Create(publisher);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Publisher({publisher.Id}) was created!");
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await _unitOfWork.Publishers
                .GetQuery()
                .Include(p => p.Games)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                throw new NotFoundException(nameof(publisher), id);
            }

            publisher.Games.Clear();

            _unitOfWork.Publishers.Delete(id);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Publisher({publisher.Id}) has been deleted!");
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
                .Include(p => p.User)
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

            if (publisher == null)
            {
                throw new NotFoundException(nameof(publisher), companyName);
            }

            var user = await _unitOfWork.Users.GetQuery().FirstOrDefaultAsync(u => u.Username == updatePublisherDTO.Username);
            publisher.User = user ?? throw new NotFoundException(nameof(user), updatePublisherDTO.Username);

            _mapper.Map(updatePublisherDTO, publisher);

            await _unitOfWork.SaveAsync();

            _logger.Info($"Publisher({publisher.Id}) has been updated!");
        }

        public async Task<bool> IsUserAssociatedWithPublisherAsync(string userObjectId, string companyName)
        {
            var publisher = await _unitOfWork.Publishers
                .GetQuery()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.CompanyName == companyName);

            if (publisher == null)
            {
                throw new BadRequestException("You do not have a publisher profile yet!");
            }

            return publisher.User.ObjectId == userObjectId;
        }

        public async Task<bool> IsGameAssociatedWithPublisherAsync(string userObjectId, string gameKey)
        {
            return await _unitOfWork.Publishers
               .GetQuery()
               .AnyAsync(p => p.User.ObjectId == userObjectId && p.Games.Any(g => g.Key == gameKey));
        }

        public async Task<string> GetCurrentCompanyNameAsync(string userObjectId)
        {
            var currentPulisher = await _unitOfWork.Publishers
                .GetQuery()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.User.ObjectId == userObjectId);

            return currentPulisher?.CompanyName;
        }

        public async Task<IEnumerable<string>> GetFreePublisherUsernamesAsync()
        {
            var publishersUserIds = await _unitOfWork.Publishers.GetQuery().Select(p => p.UserId).ToListAsync();

            var usernames = await _unitOfWork.Users
                .GetQuery()
                .Where(u => u.Role.Name == PublisherRoleName && !publishersUserIds.Contains(u.Id))
                .Select(u => u.Username)
                .ToListAsync();

            return usernames;
        }
    }
}
