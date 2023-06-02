using GameStore.BLL.DTOs.Role;

namespace GameStore.BLL.DTOs.User
{
    public class GetUserDTO : BaseUserDTO
    {
        public string Email { get; set; }

        public string ObjectId { get; set; }

        public GetRoleDTO Role { get; set; }
    }
}
