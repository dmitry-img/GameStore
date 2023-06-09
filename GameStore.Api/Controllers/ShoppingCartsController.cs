using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/shopping-carts")]
    [Authorize]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IValidationService _validationService;
        private readonly IValidator<CreateShoppingCartItemDTO> _createShoppingCartItemValidator;

        public ShoppingCartsController(
            IShoppingCartService shoppingCartService,
            IValidationService validationService,
            IValidator<CreateShoppingCartItemDTO> createShoppingCartItemValidator)
        {
            _shoppingCartService = shoppingCartService;
            _validationService = validationService;
            _createShoppingCartItemValidator = createShoppingCartItemValidator;
        }

        [HttpGet]
        [Route("items")]
        public async Task<IHttpActionResult> GetAll()
        {
            var items = await _shoppingCartService.GetAllItemsAsync();

            return Json(items);
        }

        [HttpGet]
        [Route("quantity/{gameKey}")]
        public async Task<IHttpActionResult> GetGameQuantity(string gameKey)
        {
            return Ok(await _shoppingCartService.GetGameQuantityByKeyAsync(gameKey));
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IHttpActionResult> AddItem([FromBody] CreateShoppingCartItemDTO itemDTO)
        {
            _validationService.Validate(itemDTO, _createShoppingCartItemValidator);

            await _shoppingCartService.AddItemAsync(itemDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete-item/{gameKey}")]
        public async Task<IHttpActionResult> DeleteItem(string gameKey)
        {
            await _shoppingCartService.DeleteItemAsync(gameKey);

            return Ok();
        }
    }
}
