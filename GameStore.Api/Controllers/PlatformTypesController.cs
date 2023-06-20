using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Interfaces;

using static GameStore.Shared.Infrastructure.Constants;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/platform-types")]
    [Authorize(Roles = ManagerRoleName)]
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
    }
}
