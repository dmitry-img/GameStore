using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.Api.Interfaces;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/orders")]
    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IValidationService _validationService;
        private readonly IValidator<UpdateOrderDTO> _updateOrderValidator;

        public OrdersController(
            IOrderService orderService,
            IValidationService validationService,
            IValidator<UpdateOrderDTO> updateOrderValidator)
        {
            _orderService = orderService;
            _validationService = validationService;
            _updateOrderValidator = updateOrderValidator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create()
        {
            var order = await _orderService.CreateAsync();

            return Ok(order);
        }

        [HttpGet]
        [Route("paginated-list")]
        public async Task<IHttpActionResult> GetAllWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            var orders = await _orderService.GetAllWithPaginationAsync(paginationDTO);

            return Json(orders);
        }

        [HttpPut]
        [Route("update/{orderId}")]
        public async Task<IHttpActionResult> Update(int orderId, UpdateOrderDTO updateOrderDTO)
        {
            _validationService.Validate(updateOrderDTO, _updateOrderValidator);

            await _orderService.UpdateAsync(orderId, updateOrderDTO);

            return Ok();
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<IHttpActionResult> GetById(int orderId)
        {
            var order = await _orderService.GetAsync(orderId);

            return Json(order);
        }
    }
}
