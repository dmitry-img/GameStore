using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int GameId { get; set; }
        public int? ParentCommentId { get; set; }

        public Game Game { get; set; }
        public Comment ParentComment { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; } = new List<Comment>();
    }
}
