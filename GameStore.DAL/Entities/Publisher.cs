using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.DAL.Entities.Common;

namespace GameStore.DAL.Entities
{
    public class Publisher : BaseAuditableEntity
    {
        [StringLength(40)]
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
