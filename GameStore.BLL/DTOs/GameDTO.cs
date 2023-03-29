using System.Collections.Generic;

namespace GameStore.BLL.DTOs
{
    public class GameDTO
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }
    }
}
