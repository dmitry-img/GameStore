﻿using GameStore.DAL.Entities.Common;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class PlatformType : BaseAuditableEntity
    {
        public string Type { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
