using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/shopping-carts")]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        [Route("items")]
        public async Task<IHttpActionResult> GetAll()
        {
            var items = await _shoppingCartService.GetAllItemsAsync();

            return Json(items);
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IHttpActionResult> AddItem([FromBody] CreateShoppingCartItemDTO itemDTO)
        {
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
