namespace GameStore.BLL.DTOs.Comment
{
    public class BaseCommentDto
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public bool HasQuote { get; set; }

        public string GameKey { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
