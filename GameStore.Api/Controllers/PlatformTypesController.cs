using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/platform-types")]
    [Authorize(Roles = "Manager")]
    public class PlatformTypesController : ApiController
    {
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreatePlatformTypeDTO> _createPlatformTypeValidator;
        private readonly IValidator<UpdatePlatformTypeDTO> _updatePlatformTypeValidator;

        public PlatformTypesController(
            IPlatformTypeService platformTypeService,
            IValidationService validationService,
            IValidator<CreatePlatformTypeDTO> createPlatformTypeValidator,
            IValidator<UpdatePlatformTypeDTO> updatePlatformTypeValidator)
        {
            _platformTypeService = platformTypeService;
            _validationService = validationService;
            _createPlatformTypeValidator = createPlatformTypeValidator;
            _updatePlatformTypeValidator = updatePlatformTypeValidator;
        }

        [HttpGet]
        [Route("list")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAll()
        {
            return Json(await _platformTypeService.GetAllAsync());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(CreatePlatformTypeDTO createPlatformTypeDTO)
        {
            _validationService.Validate(createPlatformTypeDTO, _createPlatformTypeValidator);

            await _platformTypeService.CreateAsync(createPlatformTypeDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdatePlatformTypeDTO updatePlatformTypeDTO)
        {
            _validationService.Validate(updatePlatformTypeDTO, _updatePlatformTypeValidator);

            await _platformTypeService.UpdateAsync(id, updatePlatformTypeDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _platformTypeService.DeleteAsync(id);

            return Ok();
        }
    }
}
