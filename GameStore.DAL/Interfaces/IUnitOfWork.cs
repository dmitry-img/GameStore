using GameStore.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }

        IGenericRepository<Comment> Comments { get; }

        IGenericRepository<Genre> Genres { get; }

        IGenericRepository<PlatformType> PlatformTypes { get; }

        IGenericRepository<Publisher> Publishers { get; }

        IGenericRepository<Order> Orders { get; }

        Task SaveAsync();
    }
}
