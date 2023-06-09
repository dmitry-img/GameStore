﻿using GameStore.DAL.Entities.Common;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Comment : BaseAuditableEntity
    {
        public string Body { get; set; }

        public bool HasQuote { get; set; }

        public int GameId { get; set; }

        public int? ParentCommentId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public Game Game { get; set; }

        public Comment ParentComment { get; set; }

        public ICollection<Comment> ChildComments { get; set; } = new List<Comment>();
    }
}
