using GameStore.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        ICommentRepository Comments { get; }
        IGenreRepository Genres { get; }
        IPlatformTypeRepository PlatformTypes { get; }        
        Task SaveAsync();
    }
}
