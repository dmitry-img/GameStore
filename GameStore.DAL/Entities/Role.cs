using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities.Common;

namespace GameStore.DAL.Entities
{
    public class Role : BaseAuditableEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
