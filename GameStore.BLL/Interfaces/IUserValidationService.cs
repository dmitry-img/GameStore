namespace GameStore.BLL.Interfaces
{
    public interface IUserValidationService
    {
        void CheckUserExistsByEmail(string email);

        void CheckUserExistsByUsername(string username);
    }
}
