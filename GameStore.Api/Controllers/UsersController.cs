using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.Ban;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateUserDTO> _createUserValidator;
        private readonly IValidator<UpdateUserDTO> _updateUserValidator;

        public UsersController(
            IUserService userService,
            IValidationService validationService,
            IValidator<CreateUserDTO> createUserValidator,
            IValidator<UpdateUserDTO> updateUserValidator)
        {
            _userService = userService;
            _validationService = validationService;
            _createUserValidator = createUserValidator;
            _updateUserValidator = updateUserValidator;
        }

        [HttpGet]
        [Route("paginated-list")]
        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> GetAllWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            var users = await _userService.GetAllWithPaginationAsync(paginationDTO);

            return Json(users);
        }

        [HttpGet]
        [Route("{objectId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> GetById(string objectId)
        {
            var user = await _userService.GetByIdAsync(objectId);

            return Json(user);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Create(CreateUserDTO createUserDTO)
        {
            _validationService.Validate(createUserDTO, _createUserValidator);

            await _userService.CreateAsync(createUserDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{objectId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Update(string objectId, UpdateUserDTO updateUserDTO)
        {
            _validationService.Validate(updateUserDTO, _updateUserValidator);

            await _userService.UpdateAsync(objectId, updateUserDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{objectId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> Delete(string objectId)
        {
            await _userService.DeleteAsync(objectId);

            return Ok();
        }

        [HttpPost]
        [Route("ban")]
        [Authorize(Roles = "Moderator")]
        public async Task<IHttpActionResult> Ban(BanDTO banDTO)
        {
            await _userService.BanAsync(banDTO);

            return Ok();
        }

        [HttpGet]
        [Route("is-banned")]
        [Authorize]
        public async Task<IHttpActionResult> IsBanned()
        {
            bool isBanned = await _userService.IsBannedAsync();

            return Ok(isBanned);
        }
    }
}
