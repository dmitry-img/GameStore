using System;
using System.Collections.Generic;
using GameStore.DAL.Entities.Common;

namespace GameStore.DAL.Entities
{
    public class User : BaseAuditableEntity
    {
        public string ObjectId { get; set; } = Guid.NewGuid().ToString();

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpirationDate { get; set; }

        public DateTime? BanEndDate { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
