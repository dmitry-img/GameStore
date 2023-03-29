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

        public UnitOfWork(string connectionString)
        {
            _context = new GameStoreDbContext(connectionString);
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
