using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.Repositories
{
    public class PlatformTypeRepository : IGenericRepository<PlatformType>
    {
        private GameStoreDbContext _context;

        public PlatformTypeRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public void Create(PlatformType item)
        {
            _context.PlatformTypes.Add(item);
        }

        public void Delete(int id)
        {
            var platformType = _context.PlatformTypes.Find(id);
            if (platformType != null)
                _context.PlatformTypes.Remove(platformType);
        }

        public PlatformType Get(int id)
        {
            return _context.PlatformTypes.Find(id);
        }

        public IEnumerable<PlatformType> GetAll()
        {
            return _context.PlatformTypes;
        }

        public void Update(PlatformType item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
