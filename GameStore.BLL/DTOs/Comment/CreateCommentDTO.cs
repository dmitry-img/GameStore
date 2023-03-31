using System;

namespace GameStore.BLL.DTOs.Comment
{
    public class CreateCommentDTO
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public Guid GameKey { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
