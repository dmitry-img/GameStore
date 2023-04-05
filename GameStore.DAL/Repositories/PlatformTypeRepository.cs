using GameStore.DAL.Data;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameStore.DAL.Repositories
{
    public class PlatformTypeRepository : GenericRepository<PlatformType>, IPlatformTypeRepository
    {
        private GameStoreDbContext _context;

        public PlatformTypeRepository(GameStoreDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
