using BigShop.Models.PaymentMethod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigShop.Repository
{
    public interface IPaymentMethodRepository
    {
        Task<int> CreateAsync(PaymentMethodCreate paymentMethodCreate);
        Task<int> DeleteAsync(int paymentMethodId);
        Task<List<PaymentMethod>> GetAllAsync();
        Task<PaymentMethod> GetByIdAsync(int paymentMethodId);
        Task<int> UpdateAsync(PaymentMethod updatedPaymentMethod);
    }
}