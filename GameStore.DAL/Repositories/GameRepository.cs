using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private GameStoreDbContext _context;

        public GameRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public void Create(Game item)
        {
           _context.Games.Add(item);
        }

        public void Delete(int id)
        {
            var game = _context.Games.Find(id);
            if (game != null)
                _context.Games.Remove(game);
        }

        public Game Get(int id)
        {
            return _context.Games.Find(id);
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games;
        }

        public void Update(Game item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
