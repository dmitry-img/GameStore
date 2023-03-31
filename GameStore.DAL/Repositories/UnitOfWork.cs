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
        private GameGenreRepository _gameGenreRepository;
        private GamePlatformTypeRepository _gamePlatformTypeRepository;

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

        public IGameGenreRepository GameGenre
        {
            get
            {
                if (_gameGenreRepository == null)
                    _gameGenreRepository = new GameGenreRepository(_context);
                return _gameGenreRepository;
            }
        }

        public IGamePlatformTypeRepository GamePlatformType
        {
            get
            {
                if (_gamePlatformTypeRepository == null)
                    _gamePlatformTypeRepository = new GamePlatformTypeRepository(_context);
                return _gamePlatformTypeRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(CancellationToken.None);
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
