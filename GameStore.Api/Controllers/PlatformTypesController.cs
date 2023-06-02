using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/platform-types")]
    [Authorize(Roles = "Manager")]
    public class PlatformTypesController : ApiController
    {
        private readonly IPlatformTypeService _platformTypeService;

        public PlatformTypesController(IPlatformTypeService platformTypeService)
        {
            _platformTypeService = platformTypeService;
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
            await _platformTypeService.CreateAsync(createPlatformTypeDTO);

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IHttpActionResult> Update(int id, UpdatePlatformTypeDTO updatePlatformTypeDTO)
        {
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
