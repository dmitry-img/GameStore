using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private GameStoreDbContext _context;

        public CommentRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public void Create(Comment item)
        {
            _context.Comments.Add(item);
        }

        public void Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
                _context.Comments.Remove(comment);
        }

        public Comment Get(int id)
        {
            return _context.Comments.Find(id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments;
        }

        public void Update(Comment item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
