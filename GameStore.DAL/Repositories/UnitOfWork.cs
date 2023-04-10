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
        }

        public IGameRepository Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new Lazy<IGameRepository>(() => 
                                            new GameRepository(_context));
                return _gameRepository.Value;
            }
        }

        public IGenericRepository<Comment> Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new Lazy<IGenericRepository<Comment>>(() => 
                                            new GenericRepository<Comment>(_context));
                return _commentRepository.Value;
            }
        }

        public IGenericRepository<Genre> Genres
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new Lazy<IGenericRepository<Genre>>(() => 
                                            new GenericRepository<Genre>(_context)); 
                return _genreRepository.Value;
            }
        }

        public IGenericRepository<PlatformType> PlatformTypes
        {
            get
            {
                if (_platformTypeRepository == null)
                    _platformTypeRepository = new Lazy<IGenericRepository<PlatformType>>(() =>
                                            new GenericRepository<PlatformType>(_context));
                return _platformTypeRepository.Value;
            }
        }

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
