using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;

namespace GameStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private GameStoreDbContext _context;
        private GameRepository _gameRepository;
        private CommentRepository _commentRepository;
        private GenreRepository _genreRepository;
        private PlatformTypeRepository _platformTypeRepository;

        public UnitOfWork(string connectionString)
        {
            _context = new GameStoreDbContext(connectionString);
        }
        public IRepository<Game> Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GameRepository(_context);
                return _gameRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_context);
                return _commentRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenreRepository(_context);
                return _genreRepository = new GenreRepository(_context);
                ;
            }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get
            {
                if (_platformTypeRepository == null)
                    _platformTypeRepository = new PlatformTypeRepository(_context);
                return _platformTypeRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
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
