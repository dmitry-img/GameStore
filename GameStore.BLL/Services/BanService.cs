using System.Threading.Tasks;
using GameStore.BLL.DTOs.Ban;
using GameStore.BLL.Interfaces;
using log4net;

namespace GameStore.BLL.Services
{
    public class BanService : IBanService
    {
        private ILog _logger;

        public BanService(ILog logger)
        {
            _logger = logger;
        }

        public Task BanAsync(BanDTO banDTO)
        {
            _logger.Info($"User who sent the comment({banDTO.CommentId}) " +
                $"has been banned! Ban duration: {banDTO.BanDuration}");
            return Task.CompletedTask;
        }
    }
}
