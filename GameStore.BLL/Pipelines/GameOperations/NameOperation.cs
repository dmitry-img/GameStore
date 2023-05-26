using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Pipelines.GameOperations
{
    public class NameOperation : IOperation<IQueryable<Game>>
    {
        private readonly string _nameFragment;

        public NameOperation(string nameFragment)
        {
            _nameFragment = nameFragment;
        }

        public IQueryable<Game> Invoke(IQueryable<Game> games)
        {
            if (!string.IsNullOrEmpty(_nameFragment) && _nameFragment.Length >= 3)
            {
                return games.Where(game => game.Name.ToLower().Contains(_nameFragment.ToLower()));
            }

            return games;
        }
    }
}
