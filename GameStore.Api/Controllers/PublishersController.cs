using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.Common;
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
        private readonly IValidator<UpdatePublisherDTO> _updatePublisherValidator;

        public PublishersController(
            IPublisherService publisherService,
            IValidationService validationService,
            IValidator<CreatePublisherDTO> createPublisherValidator,
            IValidator<UpdatePublisherDTO> updatePublisherValidator)
        {
            _publisherService = publisherService;
            _validationService = validationService;
            _createPublisherValidator = createPublisherValidator;
            _updatePublisherValidator = updatePublisherValidator;
        }

        [HttpGet]
        [Route("{companyName}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetByCompanyName(string companyName)
        {
            var publisher = await _publisherService.GetByCompanyNameAsync(companyName);

            return Json(publisher);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Manager")]
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

        [HttpGet]
        [Route("paginated-list")]
        [Authorize(Roles = "Manager")]
        public async Task<IHttpActionResult> GetAllBriefWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            var publishers = await _publisherService.GetAllBriefWithPaginationAsync(paginationDTO);

            return Json(publishers);
        }

        [HttpPut]
        [Route("update/{companyName}")]
        [Authorize(Roles = "Manager,Publisher")]
        public async Task<IHttpActionResult> Update(string companyName, UpdatePublisherDTO updatePublisherDTO)
        {
            _validationService.Validate(updatePublisherDTO, _updatePublisherValidator);

            await _publisherService.UpdateAsync(companyName, updatePublisherDTO);

            return Ok();
        }

        [HttpGet]
        [Route("{companyName}/is-user-associated-with-publisher")]
        [Authorize(Roles = "Publisher")]
        public async Task<IHttpActionResult> IsUserAssiciatedWithPublisher(string companyName)
        {
            var isAssociated = await _publisherService.IsUserAssociatedWithPublisherAsync(companyName);

            return Ok(isAssociated);
        }

        [HttpGet]
        [Route("{gameKey}/is-game-associated-with-publisher")]
        [Authorize(Roles = "Publisher")]
        public async Task<IHttpActionResult> IsGameAssiciatedWithPublisher(string gameKey)
        {
            var isAssociated = await _publisherService.IsGameAssociatedWithPublisherAsync(gameKey);

            return Ok(isAssociated);
        }

        [HttpGet]
        [Route("current")]
        [Authorize(Roles = "Publisher")]
        public async Task<IHttpActionResult> GetCurrentPublisherCompanyName()
        {
            var currentPublisherCompanyName = await _publisherService.GetCurrentCompanyNameAsync();

            return Ok(currentPublisherCompanyName);
        }

        [HttpGet]
        [Route("free")]
        [Authorize(Roles = "Manager")]
        public async Task<IHttpActionResult> GetFreePublisherUsernames()
        {
            var usernames = await _publisherService.GetFreePublisherUsernamesAsync();

            return Json(usernames);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _publisherService.DeleteAsync(id);

            return Ok();
        }
    }
}
