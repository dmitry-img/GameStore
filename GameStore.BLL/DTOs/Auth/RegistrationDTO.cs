namespace GameStore.BLL.DTOs.Auth
{
    public class RegistrationDTO : BaseAuthDTO
    {
        public string Email { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
