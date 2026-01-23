using TravelPackageManagementSystem.Repository.Models;

namespace TravelPackageManagementSystem.Services.Interface
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetTransactionHistoryAsync();
    }
}
