using GameStore.BLL.Enums;

namespace GameStore.BLL.DTOs.Ban
{
    public class BanDTO
    {
        public int CommentId { get; set; }

        public BanDuration BanDuration { get; set; }
    }
}
