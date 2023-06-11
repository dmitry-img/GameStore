using System.Linq;
using GameStore.BLL.Exceptions;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.Services
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CheckUserExistsByEmail(string email)
        {
            var user = _unitOfWork.Users.GetQuery().FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                throw new BadRequestException($"User with email {email} already exists!");
            }
        }

        public void CheckUserExistsByUsername(string username)
        {
            var user = _unitOfWork.Users.GetQuery().FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                throw new BadRequestException($"User with username {username} already exists!");
            }
        }
    }
}
