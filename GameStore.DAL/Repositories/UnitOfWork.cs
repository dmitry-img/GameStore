using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _context;
        private readonly Lazy<IGameRepository> _gameRepository;
        private readonly Lazy<IGenericRepository<Comment>> _commentRepository;
        private readonly Lazy<IGenericRepository<Genre>> _genreRepository;
        private readonly Lazy<IGenericRepository<PlatformType>> _platformTypeRepository;
        private readonly Lazy<IGenericRepository<Publisher>> _publisherRepository;
        private readonly Lazy<IGenericRepository<Order>> _orderRepository;
        private readonly Lazy<IGenericRepository<User>> _userRepository;
        private readonly Lazy<IGenericRepository<Role>> _roleRepository;
        private bool _disposed = false;

        public UnitOfWork(
            GameStoreDbContext context,
            Lazy<IGameRepository> gameRepository,
            Lazy<IGenericRepository<Comment>> commentRepository,
            Lazy<IGenericRepository<Genre>> genreRepository,
            Lazy<IGenericRepository<PlatformType>> platformTypeRepository,
            Lazy<IGenericRepository<Publisher>> publisherRepository,
            Lazy<IGenericRepository<Order>> orderRepository,
            Lazy<IGenericRepository<User>> userRepository,
            Lazy<IGenericRepository<Role>> roleRepository)
        {
            _context = context;
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
            _genreRepository = genreRepository;
            _platformTypeRepository = platformTypeRepository;
            _publisherRepository = publisherRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public IGameRepository Games => _gameRepository.Value;

        public IGenericRepository<Comment> Comments => _commentRepository.Value;

        public IGenericRepository<Genre> Genres => _genreRepository.Value;

        public IGenericRepository<PlatformType> PlatformTypes => _platformTypeRepository.Value;

        public IGenericRepository<Publisher> Publishers => _publisherRepository.Value;

        public IGenericRepository<Order> Orders => _orderRepository.Value;

        public IGenericRepository<User> Users => _userRepository.Value;

        public IGenericRepository<Role> Roles => _roleRepository.Value;

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync(CancellationToken.None);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
