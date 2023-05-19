using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/publishers")]
    public class PublishersController : ApiController
    {
        private readonly IPublisherService _publisherService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreatePublisherDTO> _createPublisherValidator;

        public PublishersController(
            IPublisherService publisherService, 
            IValidationService validationService, 
            IValidator<CreatePublisherDTO> createPublisherValidator)
        {
            _publisherService = publisherService;
            _validationService = validationService;
            _createPublisherValidator = createPublisherValidator;
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
            _validationService.Validate(publisherDTO, _createPublisherValidator);

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
