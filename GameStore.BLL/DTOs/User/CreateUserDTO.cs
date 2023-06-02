namespace GameStore.BLL.DTOs.User
{
    public class CreateUserDTO : UpdateUserDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
