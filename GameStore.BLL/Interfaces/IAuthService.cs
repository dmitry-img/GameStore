using System.Threading.Tasks;
using GameStore.BLL.DTOs.Auth;

namespace GameStore.BLL.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDTO registrationDTO);

        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);

        Task<AuthResponseDTO> RefreshAsync(string refreshToken);

        Task LogoutAsync(string userObjectId);
    }
}
