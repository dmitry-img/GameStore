using System;
using System.Collections.Generic;

namespace GameStore.BLL.DTOs
{
    public class GetCommentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public string GameKey { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
