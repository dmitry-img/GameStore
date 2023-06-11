using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Role;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/roles")]
    [Authorize(Roles = "Administrator")]
    public class RolesController : ApiController
    {
        private readonly IRoleService _roleService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateRoleDTO> _createRoleValidator;
        private readonly IValidator<UpdateRoleDTO> _updateRoleValidator;

        public RolesController(
            IRoleService roleService,
            IValidationService validationService,
            IValidator<CreateRoleDTO> createRoleValidator,
            IValidator<UpdateRoleDTO> updateRoleValidator)
        {
            _roleService = roleService;
            _validationService = validationService;
            _createRoleValidator = createRoleValidator;
            _updateRoleValidator = updateRoleValidator;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IHttpActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();

            return Json(roles);
        }

        [HttpGet]
        [Route("paginated-list")]
        public async Task<IHttpActionResult> GetAllWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            var roles = await _roleService.GetAllWithPaginationAsync(paginationDTO);

            return Json(roles);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);

            return Json(role);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleDTO createRoleDTO)
        {
            _validationService.Validate(createRoleDTO, _createRoleValidator);

            await _roleService.CreateAsync(createRoleDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _roleService.DeleteAsync(id);

            return Ok();
        }
    }
}
