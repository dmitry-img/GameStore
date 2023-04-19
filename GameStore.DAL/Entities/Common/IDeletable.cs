using System;

namespace GameStore.DAL.Entities.Common
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}
