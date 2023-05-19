using System.Threading.Tasks;
using GameStore.BLL.DTOs.Ban;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface IBanService
    {
        Task BanAsync(BanDTO banDTO);
    }
}
