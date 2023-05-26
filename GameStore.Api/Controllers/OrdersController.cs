using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        // TODO delete when users added
        private const int DefaultCustomerId = 1;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create()
        {
            var order = await _orderService.Create(DefaultCustomerId);

            return Ok(order);
        }
    }
}
