using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/shopping-carts")]
    [Authorize]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateShoppingCartItemDTO> _createShoppingCartItemValidator;

        public ShoppingCartsController(
            IShoppingCartService shoppingCartService,
            ICurrentUserService currentUserService,
            IValidationService validationService,
            IValidator<CreateShoppingCartItemDTO> createShoppingCartItemValidator)
        {
            _shoppingCartService = shoppingCartService;
            _currentUserService = currentUserService;
            _validationService = validationService;
            _createShoppingCartItemValidator = createShoppingCartItemValidator;
        }

        [HttpGet]
        [Route("items")]
        public async Task<IHttpActionResult> GetAll()
        {
            var userObjectId = _currentUserService.GetCurrentUserObjectId();

            var items = await _shoppingCartService.GetAllItemsAsync(userObjectId);

            return Json(items);
        }

        [HttpGet]
        [Route("quantity/{gameKey}")]
        public async Task<IHttpActionResult> GetGameQuantity(string gameKey)
        {
            var userObjectId = _currentUserService.GetCurrentUserObjectId();

            return Ok(await _shoppingCartService.GetGameQuantityByKeyAsync(userObjectId, gameKey));
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IHttpActionResult> AddItem([FromBody] CreateShoppingCartItemDTO itemDTO)
        {
            _validationService.Validate(itemDTO, _createShoppingCartItemValidator);

            var userObjectId = _currentUserService.GetCurrentUserObjectId();

            await _shoppingCartService.AddItemAsync(userObjectId, itemDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete-item/{gameKey}")]
        public async Task<IHttpActionResult> DeleteItem(string gameKey)
        {
            var userObjectId = _currentUserService.GetCurrentUserObjectId();

            await _shoppingCartService.DeleteItemAsync(userObjectId, gameKey);

            return Ok();
        }
    }
}
