using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Interfaces;
using GameStore.Shared;
using GameStore.Shared.Infrastructure;

using static GameStore.Shared.Infrastructure.Constants;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/shopping-carts")]
    [Authorize(Roles = UserRoleName)]
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
            var items = await _shoppingCartService.GetAllItemsAsync(UserContext.UserObjectId);

            return Json(items);
        }

        [HttpGet]
        [Route("quantity/{gameKey}")]
        public async Task<IHttpActionResult> GetGameQuantity(string gameKey)
        {
            return Ok(await _shoppingCartService.GetGameQuantityByKeyAsync(UserContext.UserObjectId, gameKey));
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IHttpActionResult> AddItem([FromBody] CreateShoppingCartItemDTO itemDTO)
        {
            _validationService.Validate(itemDTO, _createShoppingCartItemValidator);

            await _shoppingCartService.AddItemAsync(UserContext.UserObjectId, itemDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete-item/{gameKey}")]
        public async Task<IHttpActionResult> DeleteItem(string gameKey)
        {
            await _shoppingCartService.DeleteItemAsync(UserContext.UserObjectId, gameKey);

            return Ok();
        }
    }
}
