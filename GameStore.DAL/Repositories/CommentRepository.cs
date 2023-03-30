using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private GameStoreDbContext _context;

        public CommentRepository(GameStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
