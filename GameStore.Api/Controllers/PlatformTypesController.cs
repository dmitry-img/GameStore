using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    public class PlatformTypesController : ApiController
    {
        private readonly IPlatformTypeService _platformTypeService;

        public PlatformTypesController(IPlatformTypeService platformTypeService)
        {
            _platformTypeService = platformTypeService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            return Json(await _platformTypeService.GetAllAsync());
        }
    }
}
