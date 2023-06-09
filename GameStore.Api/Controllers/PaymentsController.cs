using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/payments")]
    [Authorize]
    public class PaymentsController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("bank")]
        public async Task<HttpResponseMessage> ProcessBankPayment([FromBody]int orderId)
        {
            var invoiceData = await _paymentService
                        .ProcessPayment<MemoryStream>(orderId, PaymentType.Bank);
            string fileName = $"GameStoreInvoice{orderId}.txt";

            HttpResponseMessage result =
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(invoiceData)
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            return result;
        }

        [HttpPost]
        [Route("ibox")]
        public async Task<IHttpActionResult> ProcessIboxPayment([FromBody] int orderId)
        {
            await _paymentService.ProcessPayment<int>(orderId, PaymentType.IBox);
            return Json(orderId);
        }

        [HttpPost]
        [Route("visa")]
        public async Task<IHttpActionResult> PocessVisaPayment([FromBody] int orderId)
        {
            await _paymentService.ProcessPayment<int>(orderId, PaymentType.Visa);
            return Json(orderId);
        }
    }
}
