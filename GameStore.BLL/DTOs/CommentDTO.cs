using System.Collections;
using System.Collections.Generic;

namespace GameStore.BLL.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public ICollection<CommentDTO> ChildComments { get; set; }
    }
}
