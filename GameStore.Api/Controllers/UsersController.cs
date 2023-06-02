using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/users")]
    // [Authorize(Roles = "Administrator")]
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
        public async Task<IHttpActionResult> GetAllWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            var users = await _userService.GetAllWithPaginationAsync(paginationDTO);

            return Json(users);
        }

        [HttpGet]
        [Route("{objectId}")]
        public async Task<IHttpActionResult> GetById(string objectId)
        {
            var user = await _userService.GetByIdAsync(objectId);

            return Json(user);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateUserDTO createUserDTO)
        {
            _validationService.Validate(createUserDTO, _createUserValidator);

            await _userService.CreateAsync(createUserDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{objectId}")]
        public async Task<IHttpActionResult> Update(string objectId, UpdateUserDTO updateUserDTO)
        {
            _validationService.Validate(updateUserDTO, _updateUserValidator);

            await _userService.UpdateAsync(objectId, updateUserDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{objectId}")]
        public async Task<IHttpActionResult> Delete(string objectId)
        {
            await _userService.DeleteAsync(objectId);

            return Ok();
        }
    }
}
