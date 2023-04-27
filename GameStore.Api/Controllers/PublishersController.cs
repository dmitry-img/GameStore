using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Validators;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/publishers")]
    public class PublishersController : ApiController
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        [Route("{companyName}")]
        public async Task<IHttpActionResult> GetByCompanyName(string companyName)
        {
            var publisher = await _publisherService.GetByCompanyNameAsync(companyName);

            return Json(publisher);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody] CreatePublisherDTO publisherDTO)
        {
            var validator = new CreatePublisherDTOValidator();
            var result = validator.Validate(publisherDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(string.Join(", ", errors));
            }

            await _publisherService.CreateAsync(publisherDTO);

            return Ok();
        }

        [HttpGet]
        [Route("brief")]
        public async Task<IHttpActionResult> GetAllBrief()
        {
            var publishers = await _publisherService.GetAllBriefAsync();

            return Json(publishers);
        }
    }
}
