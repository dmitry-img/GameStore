using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.Interfaces;
using GameStore.Shared;
using GameStore.Shared.Infrastructure;

using static GameStore.Shared.Infrastructure.Constants;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/orders")]
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
        [Authorize(Roles = UserRoleName)]
        public async Task<IHttpActionResult> Create()
        {
            var order = await _orderService.CreateAsync(UserContext.UserObjectId);

            return Ok(order);
        }

        [HttpGet]
        [Route("paginated-list")]
        [Authorize(Roles = ManagerRoleName)]
        public async Task<IHttpActionResult> GetAllWithPagination([FromUri] PaginationDTO paginationDTO)
        {
            var orders = await _orderService.GetAllWithPaginationAsync(paginationDTO);

            return Json(orders);
        }

        [HttpPut]
        [Route("update/{orderId}")]
        [Authorize(Roles = ManagerRoleName)]
        public async Task<IHttpActionResult> Update(int orderId, UpdateOrderDTO updateOrderDTO)
        {
            _validationService.Validate(updateOrderDTO, _updateOrderValidator);

            await _orderService.UpdateAsync(orderId, updateOrderDTO);

            return Ok();
        }

        [HttpGet]
        [Route("{orderId}")]
        [Authorize(Roles = ManagerRoleName)]
        public async Task<IHttpActionResult> GetById(int orderId)
        {
            var order = await _orderService.GetAsync(orderId);

            return Json(order);
        }
    }
}
