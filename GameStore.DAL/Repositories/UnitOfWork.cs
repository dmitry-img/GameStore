using GameStore.DAL.Data;
using GameStore.DAL.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private GameStoreDbContext _context;
        private GameRepository _gameRepository;
        private CommentRepository _commentRepository;
        private GenreRepository _genreRepository;
        private PlatformTypeRepository _platformTypeRepository;

        public UnitOfWork(GameStoreDbContext context)
        {
            _context = context;
        }

        public IGameRepository Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GameRepository(_context);
                return _gameRepository;
            }
        }

        public ICommentRepository Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_context);
                return _commentRepository;
            }
        }

        public IGenreRepository Genres
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenreRepository(_context);
                return _genreRepository;
            }
        }

        public IPlatformTypeRepository PlatformTypes
        {
            get
            {
                if (_platformTypeRepository == null)
                    _platformTypeRepository = new PlatformTypeRepository(_context);
                return _platformTypeRepository;
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
