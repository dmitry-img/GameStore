using GameStore.DAL.Entities.Common;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Comment : BaseDeletableEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
