using GameStore.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        ICommentRepository Comments { get; }
        IGameGenreRepository GameGenre { get; }
        IGamePlatformTypeRepository GamePlatformType { get; }
        
        Task SaveAsync();
    }
}
