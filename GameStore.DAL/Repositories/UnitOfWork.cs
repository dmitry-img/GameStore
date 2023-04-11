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

        public UnitOfWork(GameStoreDbContext context)
        {
            _context = context;
            _gameRepository = new Lazy<IGameRepository>(() =>
                                           new GameRepository(_context));

            _commentRepository = new Lazy<IGenericRepository<Comment>>(() =>
                                            new GenericRepository<Comment>(_context));
            
            _genreRepository = new Lazy<IGenericRepository<Genre>>(() =>
                                            new GenericRepository<Genre>(_context));

            _platformTypeRepository = new Lazy<IGenericRepository<PlatformType>>(() =>
                                               new GenericRepository<PlatformType>(_context));
        }

        public IGameRepository Games => _gameRepository.Value;
        public IGenericRepository<Comment> Comments => _commentRepository.Value;
        public IGenericRepository<Genre> Genres => _genreRepository.Value;
        public IGenericRepository<PlatformType> PlatformTypes => _platformTypeRepository.Value;

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync(CancellationToken.None);
        }

        private bool disposed = false;

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
