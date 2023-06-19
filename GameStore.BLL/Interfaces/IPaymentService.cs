using System.Threading.Tasks;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<T> ProcessPayment<T>(int orderId, PaymentType paymentType, string userObjectId);
    }
}
