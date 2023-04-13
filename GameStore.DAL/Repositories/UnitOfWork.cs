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
        private GameStoreDbContext _context;
        private Lazy<IGameRepository> _gameRepository;
        private Lazy<IGenericRepository<Comment>> _commentRepository;
        private Lazy<IGenericRepository<Genre>> _genreRepository;
        private Lazy<IGenericRepository<PlatformType>> _platformTypeRepository;
        private bool disposed = false;

        public UnitOfWork(
            GameStoreDbContext context,
            Lazy<IGameRepository> gameRepository,
            Lazy<IGenericRepository<Comment>> commentRepository,
            Lazy<IGenericRepository<Genre>> genreRepository,
            Lazy<IGenericRepository<PlatformType>> platformTypeRepository)
        {
            _context = context;
            _gameRepository = gameRepository;
            _commentRepository = commentRepository;
            _genreRepository = genreRepository;
            _platformTypeRepository = platformTypeRepository;
        }

        public IGameRepository Games => _gameRepository.Value;

        public IGenericRepository<Comment> Comments => _commentRepository.Value;

        public IGenericRepository<Genre> Genres => _genreRepository.Value;

        public IGenericRepository<PlatformType> PlatformTypes => _platformTypeRepository.Value;

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync(CancellationToken.None);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
