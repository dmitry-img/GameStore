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
        private bool _disposed = false;

        public UnitOfWork(
            GameStoreDbContext context,
            Lazy<IGameRepository> gameRepository,
            Lazy<IGenericRepository<Comment>> commentRepository,
            Lazy<IGenericRepository<Genre>> genreRepository,
            Lazy<IGenericRepository<PlatformType>> platformTypeRepository,
            Lazy<IGenericRepository<Publisher>> publisherRepository)
        {
            _context = context;
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
            _genreRepository = genreRepository;
            _platformTypeRepository = platformTypeRepository;
            _publisherRepository = publisherRepository;
        }

        public IGameRepository Games => _gameRepository.Value;

        public IGenericRepository<Comment> Comments => _commentRepository.Value;

        public IGenericRepository<Genre> Genres => _genreRepository.Value;

        public IGenericRepository<PlatformType> PlatformTypes => _platformTypeRepository.Value;

        public IGenericRepository<Publisher> Publishers => _publisherRepository.Value;

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
