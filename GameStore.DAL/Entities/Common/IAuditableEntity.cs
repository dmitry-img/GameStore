﻿using System;

namespace GameStore.DAL.Entities.Common
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }

        string CreatedBy { get; set; }

        DateTime? ModifiedAt { get; set; }

        string ModifiedBy { get; set; }

        DateTime? DeletedAt { get; set; }

        string DeteledBy { get; set; }

        bool IsDeleted { get; set; }
    }
}
